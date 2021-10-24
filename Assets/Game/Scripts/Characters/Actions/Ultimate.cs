using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class Ultimate : CoolDownAction
    {
        [SerializeField]
        int _ManaCost;

        public int manaCost => _ManaCost;

        public virtual void Activate(Hero hero)
        {
            hero.UseMana(_ManaCost);
            
            Begin(hero);
        }

        // To check if it can be used. VERY IMPORTANT. Actually everything is important. 
        public virtual bool CanUse(Hero hero)
        {
            return !isActive && hero.currentMana >= _ManaCost && !isCoolingDown;
        }
    }
}