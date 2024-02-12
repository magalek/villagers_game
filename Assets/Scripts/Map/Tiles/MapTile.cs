using System;
using UnityEngine;

namespace Map.Tiles
{
    public class MapTile : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer tileRenderer;

        public void Initialize( TileInfo info)
        {
            tileRenderer.sprite = info.sprite;
        }
    }

    public class TileInfo
    {
        public Sprite sprite;
    }
}