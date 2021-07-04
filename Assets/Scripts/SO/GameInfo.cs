using UnityEngine;

namespace HoloRPG.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/GameInfo", fileName = "GameInfo")]
    public class GameInfo : ScriptableObject
    {
        public int DefaultTurnCount;
    }
}
