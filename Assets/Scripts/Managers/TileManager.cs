using System.Collections.Generic;
using Map;
using Map.Tiles;
using Targets;
using Unity.Mathematics;
using UnityEngine;
using Utility;
using Random = UnityEngine.Random;

namespace Managers
{
    public class TileManager : MonoManager
    {
        [SerializeField] private List<Sprite> tilesSprites;
        [SerializeField] private MapTile tilePrefab;

        [SerializeField] private InputTarget haulingSpotPrefab;

        [SerializeField] private List<GatheringTarget> targetPrefabs;

        private Transform tilesParent;

        private readonly Grid<MapTile> grid = new Grid<MapTile>(32, 32, Vector2Int.zero);

        protected override void OnAwake()
        {
            tilesParent = new GameObject("Tiles Parent").transform;
            grid.Populate(CreateTile);
            grid.Get(Vector2.zero).AddObject(Instantiate(haulingSpotPrefab));

            foreach (var tile in grid)
            {
                if (tile.GridPosition == Vector2Int.zero) continue;
                
                if (Random.value > 0.9f) tile.AddObject(Instantiate(targetPrefabs.RandomItem()));
            }
        }

        public MapTile GetTile(Vector2 position) => grid.Get(position);

        private MapTile CreateTile(Vector2Int position)
        {
            var tile = Instantiate(tilePrefab, (Vector2)position, quaternion.identity);
            tile.transform.parent = tilesParent;
            tile.Initialize(position, new TileInfo {sprite = tilesSprites.RandomItem()});
            return tile;
        }
    }
}