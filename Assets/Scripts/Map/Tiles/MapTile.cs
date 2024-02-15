using System;
using System.Collections.Generic;
using Interfaces;
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
        }

        public void Click()
        {
            var targets = GetComponentsInChildren<IUIEventTarget>();
            foreach (var target in targets)
            {
                target.UIEventHandler.OnClick(this);
            }
        }

        public void StartHover()
        {
            var targets = GetComponentsInChildren<IUIEventTarget>();
            foreach (var target in targets)
            {
                target.UIEventHandler.OnStartHover(this);
            }
        }
        
        public void StopHover()
        {
            var targets = GetComponentsInChildren<IUIEventTarget>();
            foreach (var target in targets)
            {
                target.UIEventHandler.OnStopHover();
            }
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