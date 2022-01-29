using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.Interfaces
{
    public interface IPassiveEffects
    {
        Dictionary<TrackAction, PassiveEffect[]> trackActionPassiveEffects { get; }
        PassiveEffect[] passiveEffects { get; }
    }

    public interface ITrackActionEffect
    {
        TrackAction trackAction { get; }
    }

    public interface IRestrainActionEffect
    {
        RestrictAction restrictAction { get; }
    }

    public interface IStackableEffect
    {
        bool isStackable { get; }

        void Stack(params Effect[] effects);
    }

    public interface IEffectable
    {
        RestrictAction restrictedActions { get; }
        Effect[] effects { get; }
        void AddEffects(CharacterBase sender, params Effect[] effects);
        void RemoveEffects(params Effect[] effects);
    }
}