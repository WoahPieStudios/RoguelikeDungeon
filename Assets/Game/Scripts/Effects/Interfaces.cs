using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Effects
{
    public interface ICloneable<T> where T : MonoBehaviour
    {
        int instanceId { get; }
        bool isClone { get; }
        T CreateClone();
    }

    public interface IStackableEffect
    {
        void Stack(params Effect[] effects);
    }

    public interface IEffectable
    {
        Effect[] effects { get; }
        event System.Action<Effect[]> onAddEffectsEvent;
        event System.Action<Effect[]> onRemoveEffectsEvent;
        void AddEffects(IEffectable sender, params Effect[] effects);
        void RemoveEffects(params Effect[] effects);
    }
}