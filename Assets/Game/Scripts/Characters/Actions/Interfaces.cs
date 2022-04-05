using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters.Actions
{
    public interface ICoolDownAction
    {
        float coolDownTime { get; }
        float currentCoolDownTime { get; }
        bool isCoolingDown { get; }

        void StartCoolDown();
        void StopCoolDown();
    }

    public interface IActionTracker
    {
        TrackActionType trackAction { get; }
    }

    public interface IActionRestricter
    {
        RestrictActionType restrictAction { get; }
    }

    public interface IRestrictableAction
    {
        bool isRestricted { get; }
        void OnRestrict(RestrictActionType restrictActions);
    }

    public interface ITrackableAction
    {
        event System.Action<TrackActionType> onUseTrackableAction;
    }

    public interface IRestrictableActionsHandler
    {
        RestrictActionType restrictedActions { get; }
    }

    public interface ITrackableActionsHandler
    {
        TrackActionType trackedActions { get; }
    }
}