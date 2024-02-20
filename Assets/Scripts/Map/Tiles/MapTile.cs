using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UI;
using UnityEngine;

namespace Map.Tiles
{
    public class MapTile : MonoBehaviour, IGridItem
    {
        [SerializeField] private SpriteRenderer tileRenderer;

        [SerializeField] private Transform objectsParent;

        private Grid grid;
        private Vector2Int gridIndex;

        public Vector2Int GridPosition { get; private set; }

        private List<IUIEventTarget> eventTargets;

        private List<MonoBehaviour> objects = new List<MonoBehaviour>();

        public void Initialize(Vector2Int position, TileInfo info)
        {
            GridPosition = position;
            tileRenderer.sprite = info.sprite;
        }

        public void AddObject(MonoBehaviour obj)
        {
            var transform1 = obj.transform;
            transform1.parent = objectsParent;
            transform1.localPosition = Vector3.zero;
            var position = transform1.position;
            position = new Vector3(position.x, position.y, position.y);
            transform1.position = position;
            objects.Add(obj);        
            eventTargets = GetComponentsInChildren<IUIEventTarget>().ToList();
        }

        public void Click(UIContext context)
        {
            if (context.BuildingData.Value != null)
            {
                AddObject(Instantiate(context.BuildingData.Value.buildingPrefab));
                context.BuildingData.Consume();
                return;
            }


            if (eventTargets == null || eventTargets.Count == 0)
            {
                UIContextManager.Current.OnEmptyClick();
            }
            else
            {
                eventTargets.ForEach(target => target.UIEventHandler.OnClick(this));
            }
        }

        public void StartHover()
        {
            eventTargets?.ForEach(target => target.UIEventHandler.OnStartHover(this));
        }
        
        public void StopHover()
        {
            eventTargets?.ForEach(target => target.UIEventHandler.OnStopHover());
        }

        public IEnumerable<MapTile> GetAdjacent() => grid.GetAdjacentTiles(transform.position);

        public void Initialize(Grid _grid)
        {
            grid = _grid;
        }
    }

    public class TileInfo
    {
        public Sprite sprite;
    }
}