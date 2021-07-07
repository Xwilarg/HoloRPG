using HoloRPG.SO;
using System.Linq;
using UnityEngine;

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

            // Add action
            _actions[_index] = action;

            // Change color of bar in UI
            StaticResources.S.ActionBar.ChangeImageColor(
                action switch
                {
                    ActionType.MOVE => Color.blue,
                    _ => Color.black
                },
                _index);

            // Increase index
            _index++;

            return true;
        }

        public void Clear()
        {
            _index = 0;
            StaticResources.S.ActionBar.Clear();
        }

        private readonly ActionType[] _actions;
        private int _index = 0;
    }
}
