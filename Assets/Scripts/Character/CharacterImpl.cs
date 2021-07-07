using HoloRPG.Map;
using HoloRPG.Map.GridElement;
using HoloRPG.Map.Path;
using HoloRPG.SO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HoloRPG.Character
{
    public class CharacterImpl
    {
        public CharacterImpl(int x, int y, int teamId)
        {
            _team = teamId;

            _gameObject = Object.Instantiate(StaticResources.S.Resources.Character, new Vector3(x, 1f, y), Quaternion.identity);

            var ce = new CharacterElement(this);
            MapManager.S.Grid.Add(x, y, ce);
        }

        private readonly int _team; // Team the character is in
        private readonly GameObject _gameObject; // Current gameobject used for the character

        private GameObject _ghost; // Ghost display (used to display preview of actions)
        private TileDirection _lastUpdatedTile; // Last tile we hovered, we keep that here for uptimization purpose

        private readonly List<GameObject> _instanciatedTiles = new(); // Tiles that display a character available paths
        private readonly List<TileDirection> _tiles = new(); // Tiles informations
        private readonly List<GameObject> _instanciatedPath = new(); // Display path from character to current tile

        /// <summary>
        /// Called at the start of the turn, display available range etc
        /// </summary>
        public void StartTurn()
        {
            // Generate tiles
            var startingPos = RoundVector3(_gameObject.transform.position);
            _tiles.Add(new(startingPos, startingPos, int.MaxValue));
            Pathfinding.GetRange(startingPos, 10, _tiles);
            foreach (var tile in _tiles)
            {
                var go = Object.Instantiate(StaticResources.S.Resources.MovementTile, new Vector3(tile.Position.x, .01f, tile.Position.y), Quaternion.identity);
                go.transform.parent = StaticResources.S.TilesParent;
                _instanciatedTiles.Add(go);
            }

            var currCharacter = _gameObject;
            _ghost = Object.Instantiate(currCharacter, currCharacter.transform.position, currCharacter.transform.rotation);
            _ghost.name = "Ghost";
        }

        /// <summary>
        /// Called at the end of the turn, clean resources
        /// </summary>
        public void EndTurn()
        {
            foreach (var go in _instanciatedTiles) Object.Destroy(go);
            _instanciatedTiles.Clear();
            foreach (var go in _instanciatedPath) Object.Destroy(go);
            _instanciatedPath.Clear();
            _tiles.Clear();
            Object.Destroy(_ghost);
            _lastUpdatedTile = null;
        }

        public void UpdatePath(Vector2 mousePos)
        {
            var t = _tiles.FirstOrDefault(x => x.Position == mousePos);

            if (t != null && t != _lastUpdatedTile) // If we are actually overring a tile
            {
                // Clean path
                foreach (var go in _instanciatedPath) Object.Destroy(go);
                _instanciatedPath.Clear();
                DrawPath(t); // Draw it again
                _lastUpdatedTile = t;
            }
        }

        /// <summary>
        /// Vector3 to Vector2Int by rounding all values
        /// </summary>
        private static Vector2Int RoundVector3(Vector3 pos)
            => new(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y) + 1);

        /// <summary>
        /// Draw a path from the current character position to the tile given in parameter
        /// </summary>
        private void DrawPath(TileDirection tileD)
        {
            // We reached player position (only tile where from is the same as position)
            if (tileD.From == tileD.Position)
            {
                return;
            }
            var yRot = tileD.Position.x == tileD.From.x ? 90f : 0f;
            var sum = tileD.Position + tileD.From;
            var pos = new Vector3(sum.x / 2f, .5f, sum.y / 2f);
            var go = Object.Instantiate(StaticResources.S.Resources.Path, pos, Quaternion.Euler(0f, yRot, 90f));
            go.transform.parent = StaticResources.S.PathParent;
            _instanciatedPath.Add(go);
            DrawPath(_tiles.First(x => x.Position == tileD.From));
        }

        /// <summary>
        /// Draw gizmos debug
        /// </summary>
        public void DrawGizmos()
        {
            Gizmos.color = Color.red;
            foreach (var t in _tiles)
            {
                Gizmos.DrawLine(new Vector3(t.Position.x, 0f, t.Position.y), new Vector3(t.From.x, 0f, t.From.y));
            }
        }
    }
}
