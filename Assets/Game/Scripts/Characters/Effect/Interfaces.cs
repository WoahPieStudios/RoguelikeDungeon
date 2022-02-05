using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;
using Game.Characters.Actions;

namespace Game.Characters.Effects
{
    public interface ICloneable
    {
        int instanceId { get; }
        bool isClone { get; }
        T CreateClone<T>() where T : ICloneable;
    }

    public interface IEffect : ICloneable
    {
        /// <summary>
        /// The one who sent the Effect.
        /// </summary>
        IEffectable sender { get; }
        IEffectable receiver { get; }

        void StartEffect(IEffectable sender, IEffectable receiver);
        void End();
    }

    public interface IPassiveEffect : IEffect, IActionTracker
    {
        bool CanUse(Hero hero);
    }
    
    public interface IActiveEffect : IEffect, IActionRestricter
    {

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