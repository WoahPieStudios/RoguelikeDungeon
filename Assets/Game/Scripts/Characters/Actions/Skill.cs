using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class Skill : CoolDownAction
    {
        /// <summary>
        /// Start Skill.
        /// </summary>
        /// <param name="hero">The Hero who will use the Skill</param>
        public virtual void Use(Hero hero)
        {
            Begin(hero);
        }

        // To check if it can be used. VERY IMPORTANT. Actually everything is important. 
        /// <summary>
        /// Checks if the Skill can be used.
        /// </summary>
        /// <param name="hero">The Hero who will use the Skill</param>
        /// <returns></returns>
        public virtual bool CanUse(Hero hero)
        {
            return !isActive && !isCoolingDown;
        }
    }
}