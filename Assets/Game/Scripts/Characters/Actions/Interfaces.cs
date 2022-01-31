using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Actions;

namespace Game.Characters.Actions
{
    public interface ICoolDownAction : IActorAction
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

    public interface IRestrictableAction : IActorAction
    {
        bool isRestricted { get; }
        void OnRestrict(RestrictActionType restrictActions);
    }

    public interface ITrackableAction : IActorAction
    {
        event System.Action<TrackActionType> onActionEvent;
    }

    public interface IRestrictableActionsHandler
    {
        RestrictActionType restrictedActions { get; }
        void AddRestrictable(params IRestrictableAction[] restrictableActions);
        void RemoveRestrictable(params IRestrictableAction[] restrictableActions);
    }

    public interface ITrackableActionsHandler
    {
        TrackActionType trackedActions { get; }
        ITrackableAction[] trackableActions { get; }
        void AddTrackable(params ITrackableAction[] trackableActions);
        void RemoveTrackable(params ITrackableAction[] trackableActions);
    }

    public interface IMovementAction : IActorAction, IToggleAction, IRestrictableAction
    {
        Vector2 velocity { get; }

        bool Move(Vector2 direction);
    }

    public interface IOrientationAction : IActorAction, IToggleAction, IRestrictableAction
    {
        Vector2Int currentOrientation { get; }

        // Direction
        /// <summary>
        /// Orients the Character to the direction.
        /// </summary>
        /// <param name="orientation"></param>
        bool Orientate(Vector2Int orientation);
    }

    public interface IToggleAction : IActorAction
    {
        void ToggleAction(bool isActive);
    }

    public interface ICanUseAction : IActorAction
    {
        bool CanUse();
    }

    public interface IUseAction : IActorAction
    {
        bool Use();
    }

    public interface IAttackAction : ICoolDownAction, IUseAction, ICanUseAction
    {
        int damage { get; }

        /// <summary>
        /// Range of the Attack
        /// </summary>
        float range { get; }

        /// <summary>
        /// Speed of the Attack. Honestly have no idea where this would fit.
        /// </summary>
        float speed { get; }
    }

    public interface ISkillAction : ICoolDownAction, IUseAction, ICanUseAction
    {

    }

    public interface IUltimateAction : ICoolDownAction, IUseAction, ICanUseAction
    {
        int manaCost { get; }
    }
}