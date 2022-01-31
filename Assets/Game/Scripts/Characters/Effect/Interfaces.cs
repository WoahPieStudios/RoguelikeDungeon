using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;

namespace Game.Characters.Effects
{
    public interface IEffect : ICloneable
    {
        /// <summary>
        /// The one who sent the Effect.
        /// </summary>
        public IEffectable sender { get; }
        public IEffectable receiver { get; }

        void StartEffect(IEffectable sender, IEffectable receiver);
        void End();
    }

    public interface IStackableEffect : IEffect
    {
        void Stack(params IEffect[] effects);
    }

    public interface IEffectable
    {
        IEffect[] effects { get; }
        event Action<IEffect[]> onAddEffectsEvent;
        event Action<IEffect[]> onRemoveEffectsEvent;
        void AddEffects(IEffectable sender, params IEffect[] effects);
        void RemoveEffects(params IEffect[] effects);
    }

}