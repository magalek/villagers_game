using System.Collections.Generic;
using Map;
using Map.Tiles;
using Unity.Mathematics;
using UnityEngine;
using Utility;

namespace Managers
{
    public class TileManager : MonoManager
    {
        [SerializeField] private List<Sprite> tilesSprites;
        [SerializeField] private MapTile tilePrefab;
        
        private Transform tilesParent;

        private readonly MapGrid grid = new MapGrid(32, 32, Vector2Int.zero);
        
        protected override void OnAwake()
        {
            tilesParent = new GameObject("Tiles Parent").transform;
            CreateTiles();
        }

        private void CreateTiles()
        {
            foreach (var position in grid)
            {
                CreateTile(position, new TileInfo {sprite = tilesSprites.RandomItem()});
            }
        }

        private MapTile CreateTile(Vector2Int position, TileInfo info)
        {
            var tile = Instantiate(tilePrefab, (Vector2)position, quaternion.identity);
            tile.transform.parent = tilesParent;
            tile.Initialize(info);
            return tile;
        }
    }
}