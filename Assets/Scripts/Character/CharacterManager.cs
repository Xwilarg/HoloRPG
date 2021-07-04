using HoloRPG.Map.Path;
using HoloRPG.SO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HoloRPG.Character
{
    public class CharacterManager : MonoBehaviour
    {
        private readonly List<CharacterImpl> _characters = new(); // All characters
        private readonly List<GameObject> _instanciatedTiles = new(); // Tiles that display a character available paths
        private readonly List<TileDirection> _tiles = new(); // Tiles informations
        private readonly List<GameObject> _instanciatedPath = new(); // Display path from character to current tile

        private int _currentTurn = -1;

        private void Start()
        {
            _characters.Add(new CharacterImpl(2, 2, 1));
            _characters.Add(new CharacterImpl(2, 3, 1));
            _characters.Add(new CharacterImpl(10, 10, 2));

            NextTurn();
        }

        private void Update()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Get position of the mouse
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Get the tile where the mouse is
                var worldPos = hit.point;
                var mousePos = new Vector2Int(Mathf.RoundToInt(worldPos.x), Mathf.RoundToInt(worldPos.z));
                var t = _tiles.FirstOrDefault(x => x.Position == mousePos);

                if (t != null) // If we are actually overring a tile
                {
                    // TODO: Don't do that each loop
                    // Clean path
                    foreach (var go in _instanciatedPath) Destroy(go);
                    _instanciatedPath.Clear();
                    DrawPath(t); // Draw it again
                }
            }
        }

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
            _instanciatedPath.Add(Instantiate(StaticResources.S.Resources.Path, pos, Quaternion.Euler(0f, yRot, 90f)));
            DrawPath(_tiles.First(x => x.Position == tileD.From));
        }

        public void NextTurn()
        {
            _currentTurn++;
            if (_currentTurn >= _characters.Count)
            {
                _currentTurn = 0;
            }

            foreach (var go in _instanciatedTiles) Destroy(go);
            _instanciatedTiles.Clear();

            foreach (var go in _instanciatedPath) Destroy(go);
            _instanciatedPath.Clear();

            _tiles.Clear();
            var startingPos = RoundVector3(_characters[_currentTurn]._gameObject.transform.position);
            _tiles.Add(new(startingPos, startingPos, int.MaxValue));
            Pathfinding.GetRange(startingPos, 10, _tiles);
            foreach (var tile in _tiles)
            {
                _instanciatedTiles.Add(Instantiate(StaticResources.S.Resources.MovementTile, new Vector3(tile.Position.x, .01f, tile.Position.y), Quaternion.identity));
            }
        }

        private Vector2Int RoundVector3(Vector3 pos)
            => new(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y) + 1);

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            foreach (var t in _tiles)
            {
                Gizmos.DrawLine(new Vector3(t.Position.x, 0f, t.Position.y), new Vector3(t.From.x, 0f, t.From.y));
            }
        }
    }
}
