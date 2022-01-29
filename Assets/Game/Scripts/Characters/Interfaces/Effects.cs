using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters.Interfaces
{
    public interface IStackableEffect
    {
        bool isStackable { get; }

        void Stack(params Effect[] effects);
    }

    public interface IEffectable
    {
        Effect[] effects { get; }
        event Action<CharacterBase, Effect[]> onAddEffectsEvent;
        event Action<Effect[]> onRemoveEffectsEvent;
        void AddEffects(CharacterBase sender, params Effect[] effects);
        void RemoveEffects(params Effect[] effects);
    }
}