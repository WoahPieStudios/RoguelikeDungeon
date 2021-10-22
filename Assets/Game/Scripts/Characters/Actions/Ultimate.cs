using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class Ultimate : Action<Ultimate>
    {
        [SerializeField]
        int _ManaRequired;

        Hero _Hero;

        protected Hero hero => _Hero;

        public int manaRequired => _ManaRequired;

        public virtual void Activate(Hero hero)
        {
            _Hero = hero;

            hero.UseMana(_ManaRequired);
            
            Begin();
        }

        public virtual bool CanUse(Hero hero)
        {
            return !isActive && hero.currentMana >= _ManaRequired;
        }
    }
}