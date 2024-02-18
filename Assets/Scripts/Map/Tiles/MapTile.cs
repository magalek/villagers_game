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
            eventTargets = GetComponentsInChildren<IUIEventTarget>().ToList();
        }

        public void Click()
        {
            if (eventTargets == null || eventTargets.Count == 0)
            {
                UIManager.Current.OnEmptyClick();
            }
            else
            {
                eventTargets.ForEach(t => t.UIEventHandler.OnClick(this));
            }
        }

        public void StartHover()
        {
            eventTargets?.ForEach(t => t.UIEventHandler.OnStartHover(this));
        }
        
        public void StopHover()
        {
            eventTargets?.ForEach(t => t.UIEventHandler.OnStopHover());
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