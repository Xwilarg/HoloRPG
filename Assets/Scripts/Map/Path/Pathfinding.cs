using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HoloRPG.Map.Path
{
    public static class Pathfinding
    {
        private static readonly Vector2Int[] _directions = new[] { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        public static void GetRange(Vector2Int currPos, int remainingRange, IList<TileDirection> allPos)
        {
            if (remainingRange == 0)
            {
                return;
            }
            foreach (var d in _directions)
            {
                var newPos = currPos + d;
                if (newPos.x < 0 || newPos.y < 0 || newPos.x >= MapManager.S.Grid.GetGridWidth() || newPos.y >= MapManager.S.Grid.GetGridHeight())
                {
                    continue;
                }
                var elem = MapManager.S.Grid.Get(newPos.x, newPos.y);
                if (elem == null || elem.CanShadowMoveHere())
                {
                    if (!allPos.Any(x => x.Position == newPos)) // We didn't went there yet
                    {
                        allPos.Add(new(newPos, currPos, remainingRange - 1));
                        GetRange(newPos, remainingRange - 1, allPos);
                    }
                    else if (remainingRange > allPos.First(x => x.Position == newPos).Score) // Already went there but current path is shorter
                    {
                        allPos[allPos.IndexOf(allPos.First(x => x.Position == newPos))] = new(newPos, currPos, remainingRange - 1);
                        GetRange(newPos, remainingRange - 1, allPos);
                    }
                    else
                    {
                        // Nothing to do
                    }
                }
            }
        }
    }
}
