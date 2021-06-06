using System;

namespace HoloRPG.Map
{
    public class Grid
    {
        private AGridElement[,] _grid;

        public Grid(string map)
        {
            map = map.Replace('\r', '\n'); // We remove \r just in case Windows added some

            var map2d = map.Split('\n');

            if (map2d.Length == 0) // Empty file
            {
                throw new ArgumentException("Map file cannot be empty", nameof(map));
            }

            var lineLength = map2d[0].Length;
            _grid = new AGridElement[map2d.Length, lineLength];
            // Iterate on text file and load grid
            for (int y = 0; y < map.Length; y++)
            {
                var line = map2d[y];
                if (line.Length != lineLength)
                {
                    throw new ArgumentException($"Inconsistant line length, line {y} should be of length {lineLength} but is {line.Length}", nameof(map));
                }
                for (int x = 0; x < map.Length; x++)
                {
                    _grid[y, x] = line[x] switch
                    {
                        '.' => null,
                        _ => throw new ArgumentException($"Invalid tile value {line[x]} at ({x};{y})"),
                    };
                }
            }
        }
    }
}
