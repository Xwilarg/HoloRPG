using HoloRPG.SO;
using System.Linq;

namespace HoloRPG.Character.Action
{
    public class ActionPlanner
    {
        public ActionPlanner()
        {
            // Create an array of empty actions
            _actions = Enumerable.Repeat(ActionType.NONE, StaticResources.S.GameInfo.DefaultTurnCount).ToArray();
        }

        /// <summary>
        /// Add an action to the action planner
        /// </summary>
        /// <param name="action">Action to add</param>
        /// <returns>If the action can be added or not</returns>
        public bool AddAction(ActionType action)
        {
            if (_index >= _actions.Length)
            {
                return false;
            }
            _actions[_index++] = action;
            return true;
        }

        private readonly ActionType[] _actions;
        private int _index = 0;
    }
}
