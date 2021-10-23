using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public static class MethodExtensions
    {
        public static T CreateCopy<T>(this T original) where T : ScriptableObject 
        {
            return original ? Object.Instantiate(original) as T : null;
        }
    }
}