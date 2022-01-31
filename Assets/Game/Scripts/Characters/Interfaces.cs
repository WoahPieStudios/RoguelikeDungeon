using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Actions;

namespace Game.Characters
{
    public interface ICloneable
    {
        int instanceId { get; }
        bool isClone { get; }
        T CreateClone<T>() where T : ICloneable;
    }
    
    public interface IIcon
    {
        Sprite icon { get; }
    }
}