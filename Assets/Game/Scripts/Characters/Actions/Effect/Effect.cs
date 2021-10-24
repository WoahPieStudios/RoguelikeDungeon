using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class Effect : Action 
    {
        public abstract bool isStackable { get; }

        public abstract void Stack(Effect effect);

        public virtual void StartEffect(CharacterBase effected)
        {
            Begin(effected);
        }

        public virtual bool CanUse(Hero hero)
        {
            return !isActive;
        }
    }
}