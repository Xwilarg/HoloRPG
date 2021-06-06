using UnityEngine;

namespace HoloRPG.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/Resources", fileName = "Resources")]
    public class Resources : ScriptableObject
    {
        [Header("Prefabs")]
        public GameObject Character;
    }
}
