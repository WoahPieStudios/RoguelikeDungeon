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

        public virtual bool CanUse()
        {
            return !isActive && !isCoolingDown;
        }
    }
}