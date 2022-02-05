using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Actions
{
    public interface IActor
    {
        T GetProperty<T>() where T : IActorProperty;
        T[] GetProperties<T>() where T : IActorProperty;
    }

    public interface IActorProperty
    {
        IActor owner { get; }
    }
    
    public interface IActorAction : IActorProperty
    {
        bool isActive { get; }

        void End();
    }
}