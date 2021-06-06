using HoloRPG.Map.GridElement;
using UnityEngine;

namespace HoloRPG.Map
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager S { private set; get; }

        private void Awake()
        {
            S = this;
            _grid = new(Resources.Load<TextAsset>("SampleMap").text);
        }

        private Grid _grid;

        public void AddToGrid(int x, int y, IGridElement element)
            => _grid.Add(x, y, element);

        private void OnDrawGizmos()
        {
            if (_grid != null)
            {
                _grid.DrawGizmos();
            }
        }
    }
}
