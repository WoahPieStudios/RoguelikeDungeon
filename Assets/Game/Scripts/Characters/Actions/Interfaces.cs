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
        event System.Action<TrackActionType> onUseTrackableAction;
    }

    public interface IRestrictableActionsHandler
    {
        RestrictActionType restrictedActions { get; }
    }

    public interface ITrackableActionsHandler
    {
        TrackActionType trackedActions { get; }
        ITrackableAction[] trackableActions { get; }
    }

    public interface IMovementAction : IActorAction, IUseAction, IRestrictableAction
    {
        float speed { get; }
        Vector2 velocity { get; }

        bool Move(Vector2 direction);
    }

    public interface IOrientationAction : IActorAction, IUseAction, IRestrictableAction
    {
        Vector2Int currentOrientation { get; }

        // Direction
        /// <summary>
        /// Orients the Character to the direction.
        /// </summary>
        /// <param name="orientation"></param>
        bool Orientate(Vector2Int orientation);
    }

    public interface IUseAction : IActorAction
    {
        bool Use();
    }

    public interface IPriorityActionsHandler
    {
        IPriorityAction currentPriorityAction { get; }
        IPriorityAction[] priorityActions { get; }
    }

    public interface IPriorityAction : IActorAction
    {
        event System.Action<IPriorityAction> onUsePriorityAction;
    }

    public interface IAttackAction : ICoolDownAction, IUseAction, ITrackableAction, IRestrictableAction, IPriorityAction
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

        int manaGainOnHit { get; }
    }

    public interface ISkillAction : ICoolDownAction, IUseAction, ITrackableAction, IRestrictableAction, IPriorityAction
    {

    }

    public interface IUltimateAction : ICoolDownAction, IUseAction, ITrackableAction, IRestrictableAction, IPriorityAction
    {
        int manaCost { get; }
    }
}