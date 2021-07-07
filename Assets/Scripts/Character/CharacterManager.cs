using System.Collections.Generic;
using UnityEngine;

namespace HoloRPG.Character
{
    public class CharacterManager : MonoBehaviour
    {
        private readonly List<CharacterImpl> _characters = new(); // All characters

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
                // Update path position, if the player clicked on the tile, we also move there
                _characters[_currentTurn].UpdatePath(mousePos, moveToPosition: Input.GetMouseButtonDown(0));
            }
        }

        public void NextTurn()
        {
            if (_currentTurn != -1) // currentTurn is -1 at the first turn of the game
            {
                _characters[_currentTurn].EndTurn();
            }

            // Set next turn
            _currentTurn++;
            if (_currentTurn >= _characters.Count)
            {
                _currentTurn = 0;
            }

            _characters[_currentTurn].StartTurn();
        }

        private void OnDrawGizmos()
        {
            if (_currentTurn != -1) // We don't display anything while we are in the editor
            {
                _characters[_currentTurn].DrawGizmos();
            }
        }
    }
}
