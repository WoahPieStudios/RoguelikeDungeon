using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class Attack : Action
    {
        [SerializeField]
        int _Damage = 0;
        [SerializeField]
        float _Range = 0;
        [SerializeField]
        float _Speed = 0;
        [SerializeField]
        ActiveEffect[] _ActiveEffects;

        /// <summary>
        /// Damage of the Attack.
        /// </summary>
        protected int damage => _Damage;

        /// <summary>
        /// Range of the Attack
        /// </summary>
        protected float range => _Range;

        /// <summary>
        /// Speed of the Attack. Honestly have no idea where this would fit.
        /// </summary>
        protected float speed => _Speed;

        /// <summary>
        /// Active Effects of the Attack. **IS NOT CASTED AUTOMATICALLY, MUST BE ADDED ON YOUR OWN**
        /// </summary>
        protected ActiveEffect[] activeEffects => _ActiveEffects;

        /// <summary>
        /// Checks if the Attack can be used.
        /// </summary>
        /// <param name="attacker"></param>
        /// <returns></returns>
        public virtual bool CanUse(CharacterBase attacker)
        {
            return !isActive;
        }

        /// <summary>
        /// Starts Attack.
        /// </summary>
        /// <param name="attacker"></param>
        public virtual void Use(CharacterBase attacker)
        {
            Begin(attacker);
        }
    }
}