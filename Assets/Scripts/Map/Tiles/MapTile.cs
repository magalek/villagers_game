using System;
using Interfaces;
using UnityEngine;

namespace Map.Tiles
{
    public class MapTile : MonoBehaviour, IGridItem
    {
        [SerializeField] private SpriteRenderer tileRenderer;

        [SerializeField] private Transform objectsParent;
        
        public Vector2Int GridPosition { get; private set; } 
        
        public void Initialize(Vector2Int position, TileInfo info)
        {
            GridPosition = position;
            tileRenderer.sprite = info.sprite;
        }

        public void AddObject(MonoBehaviour obj)
        {
            obj.transform.parent = objectsParent;
            obj.transform.localPosition = Vector3.zero;
        }

        public void Click()
        {
            var targets = GetComponentsInChildren<IClickTarget>();
            foreach (var target in targets)
            {
                target.OnClick(this);
            }
        }
    }

    public class TileInfo
    {
        public Sprite sprite;
    }
}