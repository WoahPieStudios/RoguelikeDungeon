using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class Effect : Action, IStackableEffect
    { 
        [SerializeField]
        bool _IsStackable;

        CharacterBase _Sender;

        /// <summary>
        /// The one who sent the Effect.
        /// </summary>
        protected CharacterBase sender => _Sender;

        /// <summary>
        /// To Check if Class stackable.
        /// </summary>
        /// <value></value>
        public  bool isStackable => _IsStackable;

        // 
        /// <summary>
        /// Stacks Effect. It is called whenever an effect of the same kind is added. Sends an array of effects for you to determine how to stack an effect
        /// </summary>
        /// <param name="effects">An array of the same effect</param>
        public abstract void Stack(params Effect[] effects);

        // Starts effect
        /// <summary>
        /// Starts Effect.
        /// </summary>
        /// <param name="sender">The one who sent the effect</param>
        /// <param name="effected">The one who will be cast upon</param>
        public virtual void StartEffect(CharacterBase sender, CharacterBase effected)
        {
            _Sender = sender;
            
            Begin(effected);
        }
    }
}