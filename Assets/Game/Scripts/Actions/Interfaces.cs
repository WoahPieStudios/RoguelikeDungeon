using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Actions
{
    public interface IActor
    {
        public T GetProperty<T>() where T : IActorProperty;
        public T[] GetProperties<T>() where T : IActorProperty;
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