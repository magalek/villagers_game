using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using Managers;
using Nodes;
using UI;
using UnityEngine;

namespace Map.Tiles
{
    public class MapTile : MonoBehaviour, IGridItem
    {
        [SerializeField] private SpriteRenderer tileRenderer;

        [SerializeField] private Transform objectsParent;

        public bool IsEmpty => placeables.Count == 0;
        
        public IReadOnlyCollection<IActionNode> ActionNodes => actionNodes;
        
        public Vector2Int GridPosition { get; private set; }
        
        private Grid grid;
        private Vector2Int gridIndex;

        private HashSet<IActionNode> actionNodes = new HashSet<IActionNode>();
        private HashSet<IUIEventTarget> uiTargets = new HashSet<IUIEventTarget>();
        private HashSet<IPlaceable> placeables = new HashSet<IPlaceable>();

        public void Initialize(Vector2Int position, TileInfo info)
        {
            GridPosition = position;
            tileRenderer.sprite = info.sprite;
        }

        public T GetActionNode<T>() where T : class
        {
            foreach (var node in actionNodes)
            {
                if (node is T typedNode) return typedNode;
            }
            return null;
        }

        public void AddObject(IPlaceable placeable)
        {
            var transform1 = placeable.transform;
            transform1.parent = objectsParent;
            transform1.localPosition = Vector3.zero;
            var position = transform1.position;
            position = new Vector3(position.x, position.y, position.y);
            transform1.position = position;
            placeables.Add(placeable);
            placeable.WillDestroy += OnPlaceableDestroyed;
            ReassignPlaceables();
            if (placeables.Count == 1) grid.AddActiveTile(this);
        }

        public void RemoveObject(IPlaceable placeable)
        {
            placeables.Remove(placeable);
            placeable.WillDestroy -= OnPlaceableDestroyed;
            ReassignPlaceables();
            if (placeables.Count == 0) grid.RemoveActiveTile(this);
        }

        public void Click(UIContext context)
        {
            if (context.BuildingData.Value != null)
            {
                AddObject(NodeManager.Current.CreateNewBuilding(context.BuildingData.Value));
                context.BuildingData.Consume();
                return;
            }


            if (uiTargets == null || uiTargets.Count == 0)
            {
                UIContextManager.Current.OnEmptyClick();
            }
            else
            {
                foreach (var target in uiTargets) target.UIEventHandler?.OnClick(this);
            }
        }

        public void StartHover()
        {
            foreach (var target in uiTargets) target.UIEventHandler?.OnStartHover(this);
        }
        
        public void StopHover()
        {
            foreach (var target in uiTargets) target.UIEventHandler?.OnStopHover();
        }

        public IEnumerable<MapTile> GetAdjacent() => grid.GetAdjacentTiles(transform.position);

        public void Initialize(Grid _grid)
        {
            grid = _grid;
        }

        private void OnPlaceableDestroyed(IPlaceable placeable) => RemoveObject(placeable);

        private void ReassignPlaceables()
        {
            foreach (var placeable in placeables)
            {
                if (placeable is IActionNode node) actionNodes.Add(node);
                if (placeable is IUIEventTarget target) uiTargets.Add(target);
            }
        }
    }

    public class TileInfo
    {
        public Sprite sprite;
    }
}