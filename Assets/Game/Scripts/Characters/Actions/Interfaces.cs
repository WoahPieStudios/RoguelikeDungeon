using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Upgrades;
using Game.Properties;
using Game.Actions;

namespace Game.Characters.Actions
{
    public interface ICharacterAction<T> where T : Character
    {
        T owner { get; }
    }

    public interface IMovementAction : IAction, IRestrictableAction
    {
        Property speed { get; }
        Vector2 velocity { get; }
        bool Move(Vector2 direction);
    }

    public interface IOrientationAction : IAction
    {
        Vector2Int currentOrientation { get; }
        bool Orientate(Vector2Int orientation);
    }

    public interface IAttackAction : ICoolDownAction, IRestrictableAction
    {
        Property speed { get; }
        Property damage { get; }
        Property range { get; }
    }

    public interface IHeroAttackAction : IAttackAction, ITrackableAction, IUpgradeable
    {
        Property manaGainOnHit { get; }
    }

    public interface ISkillAction : ICoolDownAction, ITrackableAction, IRestrictableAction, IUpgradeable
    {

    }

    public interface IUltimateAction : ICoolDownAction, ITrackableAction, IRestrictableAction, IUpgradeable
    {
        Property manaCost { get; }
    }

    public interface ICoolDownAction : IAction
    {
        Property coolDownTime { get; }
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