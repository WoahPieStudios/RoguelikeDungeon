using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public interface ICopyable<T> where T : Object
    {
        T CreateCopy();
    }

    public interface IStackable
    {
        bool isStackable { get; }
    }
}