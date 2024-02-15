using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Map.Tiles;
using UnityEngine;

namespace Map
{

    public class Grid : IEnumerable<MapTile>
    {
        private readonly int xSize, ySize;
        private readonly int xHalf, yHalf;
        private Vector2Int center;

        private List<MapTile> objects;

        public Grid(int _xSize, int _ySize)
        {
            if (_xSize % 2 != 0 || _ySize % 2 != 0) Debug.LogError("Grid size is not even!");
            xSize = _xSize;
            ySize = _ySize;
            xHalf = Mathf.FloorToInt(xSize / 2f);
            yHalf = Mathf.FloorToInt(ySize / 2f);
        }

        public Grid(int xSize, int ySize, Vector2Int _center) : this(xSize, ySize)
        {
            center = _center;
        }

        public void Populate(Func<Vector2Int, MapTile> builderMethod)
        {
            objects = new List<MapTile>();
            foreach (var position in EnumeratePositions())
            {
                var gridObject = builderMethod.Invoke(position);
                gridObject.Initialize(this);
                objects.Add(gridObject);
            }
        }

        public MapTile GetTile(Vector2 position)
        {
            var index = GetGridIndex(position);
            if (index >= 0 && index <= objects.Count - 1) return objects[index];
            
            Debug.LogWarning($"Tile out of range: {position}");
            return null;
        }
        
        public MapTile GetTile(Vector2Int position)
        {
            
            var index = GetGridIndex(position);
            if (index >= 0 && index <= objects.Count - 1) return objects[index];
            
            Debug.LogWarning($"Tile out of range: {position}");
            return null;
        }

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
                    yield return GetTile(normalizedPosition);
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
                    yield return GetTile(tilePosition);
                }
            }
        }

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

        public static Vector2Int NormalizePosition(Vector2 position) => new Vector2Int(Mathf.FloorToInt(position.x + 0.5f), Mathf.FloorToInt(position.y + 0.5f));

        private int GetGridIndex(Vector2Int intPosition)
        {
            // mx = x size
            // my = y size
            // formula: (x + mx/2) * mx + (y + my/2)
            return ((intPosition.x + xHalf) * xSize) + (intPosition.y + yHalf);
        }
        
        private int GetGridIndex(Vector2 position) => GetGridIndex(NormalizePosition(position));

        public IEnumerator<MapTile> GetEnumerator() => objects.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}