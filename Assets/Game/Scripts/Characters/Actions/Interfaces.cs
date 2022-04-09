using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Upgrades;

namespace Game.Characters.Actions
{
    public interface IAction
    {
        void Use();
        void End();
    }

    public interface IMovementAction : IAction, IRestrictableAction
    {
        float speed { get; }
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
        float speed { get; }
        float damage { get; }
        float range { get; }
    }

    public interface IHeroAttackAction : IAttackAction, IUpgradeable
    {
        float manaGainOnHit { get; }
    }

    public interface ISkillAction : ICoolDownAction, IRestrictableAction, IUpgradeable
    {

    }

    public interface IUltimateAction : ICoolDownAction, IRestrictableAction, IUpgradeable
    {
        float manaCost { get; }
    }

    public interface ICoolDownAction : IAction
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