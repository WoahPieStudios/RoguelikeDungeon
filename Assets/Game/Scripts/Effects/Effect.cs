using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Effects
{
    public abstract class Effect : MonoBehaviour, ICloneable<Effect>
    {
        IEffectable _Sender;
        IEffectable _Receiver;

        bool _IsClone = false;
        int _InstanceId;

        /// <summary>
        /// The one who sent the Effect.
        /// </summary>
        public IEffectable sender => _Sender;
        /// <summary>
        /// The one who sent the Effect.
        /// </summary>
        public IEffectable receiver => _Receiver;

        /// <summary>
        /// To determine if Action is a copy or not. **DO NOT CHANGE THE VALUE**
        /// </summary>
        /// <value></value>
        public bool isClone => _IsClone;

        /// <summary>
        /// Unique ID of the Action. **DO NOT CHANGE THE VALUE**
        /// </summary>
        /// <returns></returns>
        public int instanceId => _InstanceId;

        // Starts effect
        /// <summary>
        /// Starts Effect.
        /// </summary>
        /// <param name="sender">The one who sent the effect</param>
        /// <param name="receiver">The one who will be cast upon</param>
        public virtual void StartEffect(IEffectable sender, IEffectable receiver)
        {
            _Sender = sender;
            _Receiver = receiver;
        }

        public abstract void End();

        /// <summary>
        /// Creates copy. **DO NOT TOUCH**
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Effect CreateClone()
        {
            Effect effect = Instantiate(this);

            effect._IsClone = true;
            effect._InstanceId = GetInstanceID();

            return effect;
        }
    }
}