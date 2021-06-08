using HoloRPG.Map.GridElement;
using System;
using UnityEngine;

namespace HoloRPG.Map
{
    public class Grid
    {
        private IGridElement[,] _grid;

        public int GetGridHeight()
        {
            return _grid.GetLength(0);
        }

        public int GetGridWidth()
        {
            return _grid.GetLength(1);
        }

        public Grid(string map)
        {
            map = map.Replace("\r", ""); // We remove \r just in case Windows added some

            var map2d = map.Split('\n');

            if (map2d.Length == 0) // Empty file
            {
                throw new ArgumentException("Map file cannot be empty", nameof(map));
            }

            var lineLength = map2d[0].Length;
            _grid = new IGridElement[map2d.Length, lineLength];
            // Iterate on text file and load grid
            for (int y = 0; y < map2d.Length; y++)
            {
                var line = map2d[y];
                if (line.Length != lineLength)
                {
                    throw new ArgumentException($"Inconsistant line length, line {y} should be of length {lineLength} but is {line.Length}", nameof(map));
                }
                for (int x = 0; x < line.Length; x++)
                {
                    _grid[x, y] = line[x] switch
                    {
                        '.' => null,
                        'X' => new ObstacleElement(x, y),
                        _ => throw new ArgumentException($"Invalid tile value {line[x]} at ({x};{y})"),
                    };
                }
            }
        }

        /// <summary>
        /// Add an element in the grid, we assume the positions are in the grid
        /// </summary>
        public void Add(int x, int y, IGridElement element)
        {
            if (_grid[x, y] != null)
            {
                throw new ArgumentException($"Can't add an element to grid at ({x};{y}), position is not empty");
            }
            _grid[x, y] = element;
        }

        public IGridElement Get(int x, int y)
            => _grid[x, y];

        public void DrawGizmos()
        {
            for (int y = 0; y < _grid.GetLength(0); y++)
            {
                for (int x = 0; x < _grid.GetLength(1); x++)
                {
                    var elem = _grid[x, y];
                    if (elem == null)
                    {
                        Gizmos.color = Color.white;
                    }
                    else if (elem is ObstacleElement oe)
                    {
                        Gizmos.color = Color.black;
                    }
                    else if (elem is CharacterElement ce)
                    {
                        Gizmos.color = Color.blue;
                    }
                    else
                    {
                        Gizmos.color = Color.red;
                    }
                    Gizmos.DrawWireCube(new Vector3(x, 0f, y), new Vector3(1f, 0f, 1f));
                }
            }
        }
    }
}
