using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class Ultimate : CoolDownAction
    {
        [SerializeField]
        readonly int _ManaCost;

        /// <summary>
        /// Mana cost of the Ultimate
        /// </summary>
        public int manaCost => _ManaCost;

        /// <summary>
        /// Activates the Ultimate.
        /// </summary>
        /// <param name="hero">The Hero who will use the Ultimate</param>
        public virtual void Activate(Hero hero)
        {
            hero.UseMana(_ManaCost);
            
            Begin(hero);
        }

        // To check if it can be used. VERY IMPORTANT. Actually everything is important. 
        /// <summary>
        /// Checks if the ultimate can be used.
        /// </summary>
        /// <param name="hero">The Hero who will use the Ultimate</param>
        /// <returns></returns>
        public virtual bool CanUse(Hero hero)
        {
            return !isActive && hero.currentMana >= _ManaCost && !isCoolingDown;
        }
    }
}