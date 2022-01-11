using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class Effect : Action
    {
        CharacterBase _Sender;

        /// <summary>
        /// The one who sent the Effect.
        /// </summary>
        protected CharacterBase sender => _Sender;

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