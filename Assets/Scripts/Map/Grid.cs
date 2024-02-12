using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Map
{
    public class Grid<TObject> : IEnumerable<TObject> where TObject : class, IGridItem
    {
        private readonly int xSize, ySize;
        private readonly int xHalf, yHalf;
        private Vector2Int center;

        private List<TObject> objects;
        
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

        public void Populate(Func<Vector2Int, TObject> builderMethod)
        {
            objects = new List<TObject>();
            foreach (var position in EnumeratePositions())
            {
                objects.Add(builderMethod.Invoke(position));
            }
        }

        public TObject Get(Vector2 position)
        {
            // mx = x size
            // my = y size
            // formula: (x + mx/2) * mx + (y + my/2)
            var intPosition = new Vector2Int(Mathf.FloorToInt(position.x + 0.5f), Mathf.FloorToInt(position.y + 0.5f));
            var index = ((intPosition.x + xHalf) * xSize) + (intPosition.y + yHalf);
            if (index >= 0 && index <= objects.Count - 1) return objects[index];
            
            Debug.LogWarning($"Tile out of range: {position}");
            return null;
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
        
        public IEnumerator<TObject> GetEnumerator() => objects.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}