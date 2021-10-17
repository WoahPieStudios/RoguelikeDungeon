using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class Ultimate : Action
    {
        [SerializeField]
        int _ManaRequired;

        public int manaRequired => _ManaRequired;

        public virtual void Use(Hero hero)
        {
            StartAction();

            hero.UseMana(_ManaRequired);
        }
    }
}