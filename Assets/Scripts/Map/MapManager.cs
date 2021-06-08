using UnityEngine;

namespace HoloRPG.Map
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager S { private set; get; }

        private void Awake()
        {
            S = this;
            Grid = new(Resources.Load<TextAsset>("SampleMap").text);
        }

        public Grid Grid { private set; get; }

        private void OnDrawGizmos()
        {
            if (Grid != null)
            {
                Grid.DrawGizmos();
            }
        }
    }
}
