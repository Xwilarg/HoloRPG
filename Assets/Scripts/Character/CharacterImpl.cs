using HoloRPG.Map;
using HoloRPG.Map.GridElement;
using HoloRPG.SO;
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
            MapManager.S.AddToGrid(x, y, ce);
        }

        public int _team;
        public GameObject _gameObject;
    }
}
