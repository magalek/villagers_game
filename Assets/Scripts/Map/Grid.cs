using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using Map.Tiles;
using UnityEngine;

namespace Map
{
    public class Grid : IEnumerable<MapTile>
    {
        public IReadOnlyCollection<MapTile> ActiveTiles => activeTiles;

        private readonly int xSize, ySize;
        private readonly int xHalf, yHalf;
        private Vector2Int center;

        private List<MapTile> tiles = new List<MapTile>();
        private List<MapTile> activeTiles = new List<MapTile>();

        public Grid(int _xSize, int _ySize, Vector2Int _center, Func<Vector2Int, MapTile> builderMethod)
        {
            if (_xSize % 2 != 0 || _ySize % 2 != 0) Debug.LogError("Grid size is not even!");
            xSize = _xSize;
            ySize = _ySize;
            xHalf = Mathf.FloorToInt(xSize / 2f);
            yHalf = Mathf.FloorToInt(ySize / 2f);
            
            center = _center;
            foreach (var position in EnumeratePositions())
            {
                var gridObject = builderMethod.Invoke(position);
                gridObject.Initialize(this);
                tiles.Add(gridObject);
            }
        }

        public void AddActiveTile(MapTile tile) => activeTiles.Add(tile);
        public void RemoveActiveTile(MapTile tile) => activeTiles.Remove(tile);

        public IEnumerable<MapTile> GetSortedActiveTilesByDistance(Vector2Int position)
        {
            var distances = new List<(MapTile, float)>(activeTiles.Count);

            foreach (var activeTile in activeTiles)
            {
                distances.Add((activeTile, Vector2Int.Distance(activeTile.GridPosition, position)));
            }

            foreach (var (tile, _) in distances.OrderBy(pair => pair.Item2))
            {
                yield return tile;
            }
        }
        
        public IEnumerable<MapTile> GetSortedActiveTilesByDistance(Vector2 position) => GetSortedActiveTilesByDistance(NormalizePosition(position));

        public MapTile GetTileFromPosition(Vector2 position)
        {
            var index = GetGridIndex(position);
            if (index >= 0 && index <= tiles.Count - 1) return tiles[index];
            
            Debug.LogWarning($"Tile out of range: {position}");
            return null;
        }
        
        public MapTile GetTileFromPosition(Vector2Int position)
        {
            
            var index = GetGridIndex(position);
            if (index >= 0 && index <= tiles.Count - 1) return tiles[index];
            
            Debug.LogWarning($"Tile out of range: {position}");
            return null;
        }

        public MapTile GeTileFromIndex(int x, int y) => tiles[x + (y * ySize)];

        public MapTile FindFirstEmpty(Vector2 position, int maxSpan = Int32.MaxValue)
        {
            var currentPosition = NormalizePosition(position);
            
            int currentSpan = 0;

            int xIndex = 1, yIndex = 1;

            MapTile tile = GetTileFromPosition(currentPosition);
            
            while (!tile.IsEmpty)
            {
                var lastPosition = currentPosition;
                
                for (int i = 0; i < yIndex; i++)
                {
                    currentPosition.y += yIndex % 2 == 0 ? 1 : -1;
                    tile = GetTileFromPosition(currentPosition);
                    Debug.DrawLine(new Vector3(lastPosition.x, lastPosition.y), new Vector3(currentPosition.x, currentPosition.y), Color.red, 4);
                    lastPosition = currentPosition;
                    if (tile == null) return null;
                    if (tile.IsEmpty) return tile;
                }

                
                
                yIndex++;
                for (int j = 0; j < xIndex; j++)
                {
                    currentPosition.x += xIndex % 2 == 0 ? -1 : 1;
                    tile = GetTileFromPosition(currentPosition);
                    lastPosition = currentPosition;
                    Debug.DrawLine(new Vector3(lastPosition.x, lastPosition.y), new Vector3(currentPosition.x, currentPosition.y), Color.red, 4);
                    if (tile == null) return null;
                    if (tile.IsEmpty) return tile;
                }
                xIndex++;
            }
            return tile;
        }
        
        public MapTile GetEmptyAdjacent(Vector2 position, int size = 1, bool includeFirstTile = false) =>
            GetAdjacentTiles(position, size, includeFirstTile).FirstOrDefault(tile => tile.IsEmpty);
        
        public IEnumerable<MapTile> GetAdjacentTiles(Vector2 position, int size = 1, bool includeFirstTile = false) => GetAdjacentTiles(NormalizePosition(position), size, includeFirstTile);

        public IEnumerable<MapTile> GetAdjacentTiles(Vector2Int normalizedPosition, int size = 1, bool includeFirstTile = false)
        {
            switch (size)
            {
                case < 0:
                    Debug.LogWarning($"Size cannot be negative.");
                    yield return null;
                    break;
                case 0:
                    Debug.LogWarning("GetAdjacentTiles with size 0 returns one tile.");
                    yield return GetTileFromPosition(normalizedPosition);
                    break;
            }

            var span = Vector2Int.one * size;
            
            var minPosition = normalizedPosition - span;
            var maxPosition = normalizedPosition + span;

            for (int x = minPosition.x; x <= maxPosition.x; x++)
            {
                for (int y = minPosition.y; y <= maxPosition.y; y++)
                {
                    var tilePosition = new Vector2Int(x, y);
                    if (!includeFirstTile && tilePosition == normalizedPosition) continue;
                    yield return GetTileFromPosition(tilePosition);
                }
            }
        }

        public static Vector2Int NormalizePosition(Vector2 position) => new Vector2Int(Mathf.FloorToInt(position.x + 0.5f), Mathf.FloorToInt(position.y + 0.5f));

        private IEnumerable<Vector2Int> EnumeratePositions()
        {
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    yield return new Vector2Int((center.x - xHalf) + x, (center.y - yHalf) + y);
                }
            }
        }
        
        private int GetGridIndex(Vector2Int intPosition)
        {
            // mx = x size
            // my = y size
            // formula: (x + mx/2) * mx + (y + my/2)
            return ((intPosition.x + xHalf) * xSize) + (intPosition.y + yHalf);
        }
        
        private int GetGridIndex(Vector2 position) => GetGridIndex(NormalizePosition(position));

        public IEnumerator<MapTile> GetEnumerator() => tiles.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}