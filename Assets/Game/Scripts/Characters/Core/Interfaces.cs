using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Actions;
using Game.Characters.Actions;
using Game.Characters.Effects;
using Game.Characters.Properties;

namespace Game.Characters
{
    public interface ICharacterActor : IActor, IEffectable, IRestrictableActionsHandler
    {
        IHealthProperty health { get; }

        IMovementAction movement { get; }
        IOrientationAction orientation { get; }
    }

    public interface IHeroActor : ICharacterActor, ITrackableActionsHandler
    {
        IManaProperty mana{ get; }

        IAttackAction attack { get; }
        
        /// <summary>
        /// Skill of the Hero
        /// </summary>
        ISkillAction skill { get; }

        /// <summary>
        /// Ultimate of the Hero
        /// </summary>
        IUltimateAction ultimate { get; }
    }
}