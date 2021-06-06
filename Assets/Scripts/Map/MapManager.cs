using UnityEngine;

namespace HoloRPG.Map
{
    public class MapManager : MonoBehaviour
    {
        private Grid _grid;

        private void Start()
        {
            _grid = new(Resources.Load<TextAsset>("SampleMap").text);
        }

        private void OnDrawGizmos()
        {
            if (_grid != null)
            {
                _grid.DrawGizmos();
            }
        }
    }
}
