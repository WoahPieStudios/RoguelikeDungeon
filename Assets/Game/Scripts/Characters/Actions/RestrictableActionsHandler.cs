using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Effects;

namespace Game.Characters.Actions
{
    public class RestrictableActionsHandler
    {
        RestrictActionType _RestrictedActions;

        List<IRestrictableAction> _RestrictableActionsList = new List<IRestrictableAction>();
        
        public RestrictActionType restrictedActions => _RestrictedActions;

        public void GetRestrainedActions(IEnumerable<IActionRestricter> actionRestricters)
        {
            RestrictActionType restrainedActions = default;

            // Adds Restrict Actions to RestrainedActions
            foreach(RestrictActionType r in actionRestricters.Select(r => r.restrictAction))
                restrainedActions |= r;

            _RestrictedActions = restrainedActions;
        }

        public void SendRestrictionsToActions()
        {
            foreach(IRestrictableAction restrictable in _RestrictableActionsList)
                restrictable.OnRestrict(restrictedActions);
        }

        public void AddRestrictableAction(params IRestrictableAction[] restrictableActions)
        {
            _RestrictableActionsList.AddRange(restrictableActions.Where(a => !_RestrictableActionsList.Contains(a)));
        }

        public void RemoveRestrictableAction(params IRestrictableAction[] restrictableActions)
        {
            _RestrictableActionsList.RemoveAll(r => restrictableActions.Contains(r));
        }
    }
}