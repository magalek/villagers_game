using System.Collections.Generic;
using Map;
using Map.Tiles;
using Nodes;
using Targets;
using Unity.Mathematics;
using UnityEngine;
using Utility;
using Grid = Map.Grid;
using Random = UnityEngine.Random;

namespace Managers
{
    public class MapManager : MonoManager<MapManager>
    {
        [SerializeField] private List<Sprite> tilesSprites;
        [SerializeField] private MapTile tilePrefab;

        [SerializeField] private InputNode haulingSpotPrefab;

        [SerializeField] private List<GatheringNode> targetPrefabs;

        private Transform tilesParent;

        public Grid Grid { get; private set; }

        protected override void OnAwake()
        {
            Grid = new Grid(32, 32, Vector2Int.zero, CreateTile);
            tilesParent = new GameObject("Tiles Parent").transform;
            Grid.GetTileFromPosition(Vector2.zero).AddObject(Instantiate(haulingSpotPrefab));

            foreach (var tile in Grid)
            {
                if (tile.GridPosition == Vector2Int.zero) continue;
                
                if (Random.value > 0.9f) tile.AddObject(Instantiate(targetPrefabs.RandomItem()));
            }
        }

        private MapTile CreateTile(Vector2Int position)
        {
            var tile = Instantiate(tilePrefab, (Vector2)position, quaternion.identity);
            tile.transform.parent = tilesParent;
            tile.Initialize(position, new TileInfo {sprite = tilesSprites.RandomItem()});
            return tile;
        }
    }
}