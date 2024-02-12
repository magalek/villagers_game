using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapGrid : IEnumerable<Vector2Int>
    {
        private int xSize, ySize;
        private Vector2Int center;

        private int xHalf, yHalf;
        public MapGrid(int _xSize, int _ySize)
        {
            if (_xSize % 2 != 0 || _ySize % 2 != 0) Debug.LogError("Grid size is not even!");
            xSize = _xSize;
            ySize = _ySize;
            xHalf = xSize / 2;
            yHalf = ySize / 2;
        }

        public MapGrid(Vector2Int size)
        {
            xSize = size.x;
            ySize = size.y;
            xHalf = xSize / 2;
            yHalf = ySize / 2;
        }

        public MapGrid(int xSize, int ySize, Vector2Int _center) : this(xSize, ySize)
        {
            center = _center;
        }

        public IEnumerator<Vector2Int> GetEnumerator()
        {
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    yield return new Vector2Int((center.x - xHalf) + x, (center.y - yHalf) + y);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}