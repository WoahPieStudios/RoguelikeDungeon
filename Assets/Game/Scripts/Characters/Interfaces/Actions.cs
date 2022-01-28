using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.Interfaces
{
    public interface IAction<T>
    {
        bool isActive { get; }
        T target { get; }

        void End();
    }

    public interface IOnAssignEvent
    {
        void OnAssign(CharacterBase character);
    }

    public interface ICloneable
    {
        int instanceId { get; }
        bool isCopied { get; }
        T CreateClone<T>() where T : Object, ICloneable;
        void InitializeClone(int instanceid);
    }

    public interface ICoolDown
    {
        float coolDownTime { get; }
        float currentCoolDownTime { get; }
        bool isCoolingDown { get; }
    }
    
    public interface IIcon
    {
        Sprite icon { get; }
    }

    public interface IActionTracker
    {
        TrackAction trackAction { get; }
    }

    public interface IActionRestricter
    {
        RestrictAction restrictAction { get; }
    }

    public interface IRestrictableAction
    {
        void OnRestrict(RestrictAction restrictActions);
    }

    public interface ITrackableAction
    {
        event System.Action<TrackAction> onActionTracked;
    }

    public interface IRestrictableActionsHandler
    {
        RestrictAction restrictedActions { get; }
        IRestrictableAction[] restrictableActions { get; }
        void AddRestrictable(params IRestrictableAction[] restrictableActions);
        void RemoveRestrictable(params IRestrictableAction[] restrictableActions);
    }

    public interface ITrackableActionsHandler
    {
        TrackAction trackedActions { get; }
        ITrackableAction[] trackableActions { get; }
        void AddTrackable(params ITrackableAction[] trackableActions);
        void RemoveTrackable(params ITrackableAction[] trackableActions);
    }
}