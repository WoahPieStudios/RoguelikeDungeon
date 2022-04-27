using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Upgrades;
using Game.Properties;
using Game.Actions;

namespace Game.Characters.Actions
{
    public interface IHeroMovementAction : IMovementAction, IUpgradeable
    {
        
    }

    public interface IHeroAttackAction : IAttackAction, IUpgradeable, ITrackableAction
    {
        Property manaGainOnHit { get; }
    }

    public interface ISkillAction : ICoolDownAction, IUpgradeable, ITrackableAction
    {

    }

    public interface IUltimateAction : ICoolDownAction, IUpgradeable, ITrackableAction
    {
        Property manaCost { get; }
    }
}