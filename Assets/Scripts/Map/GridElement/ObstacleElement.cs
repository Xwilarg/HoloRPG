using HoloRPG.SO;
using UnityEngine;

namespace HoloRPG.Map.GridElement
{
    public class ObstacleElement : IGridElement
    {
        public ObstacleElement(int x, int y)
        {
            _gameObject = Object.Instantiate(StaticResources.S.Resources.Rock, new Vector3(x, .5f, y), Quaternion.identity);
        }

        private readonly GameObject _gameObject;

        public bool CanShadowMoveHere()
            => false;
    }
}
