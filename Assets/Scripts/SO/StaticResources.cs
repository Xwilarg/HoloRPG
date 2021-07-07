using UnityEngine;

namespace HoloRPG.SO
{
    public class StaticResources : MonoBehaviour
    {
        public static StaticResources S { private set; get; }

        private void Awake()
        {
            S = this;
            PathParent = new GameObject("Path").transform;
            TilesParent = new GameObject("Tiles").transform;
        }

        public ActionBar ActionBar;

        public Resources Resources;
        public GameInfo GameInfo;

        public Transform TilesParent { private set; get; }
        public Transform PathParent { private set; get; }
    }
}
