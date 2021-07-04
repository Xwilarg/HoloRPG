using UnityEngine;

namespace HoloRPG.SO
{
    public class StaticResources : MonoBehaviour
    {
        public static StaticResources S { private set; get; }

        private void Awake()
        {
            S = this;
        }

        public Resources Resources;
        public GameInfo GameInfo;
    }
}
