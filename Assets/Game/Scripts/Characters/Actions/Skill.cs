using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class Skill : CoolDownAction
    {
        public virtual void Use(Hero hero)
        {
            Begin(hero);
        }

        // To check if it can be used. VERY IMPORTANT. Actually everything is important. 
        public virtual bool CanUse(Hero hero)
        {
            return !isActive && !isCoolingDown;
        }
    }
}