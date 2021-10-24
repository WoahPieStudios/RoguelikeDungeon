using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class Effect : Action, IStackable
    { 
        // To Check if Class stackable, I suggest to assign it directly since I made the system to check if an effect class is stackable. E.G. public override bool isStackable => true;
        public abstract bool isStackable { get; }

        // Sends an array of effects for you to determine how to stack an effect
        public abstract void Stack(params Effect[] effects);

        // Starts effect
        public virtual void StartEffect(CharacterBase effected)
        {
            Begin(effected);
        }
    }
}