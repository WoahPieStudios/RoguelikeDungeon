using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class Effect : ScriptableObject
    {
        [SerializeField]
        RestrictAction _RestrictAction;

        Character _Character;

        public RestrictAction restrictAction => _RestrictAction;

        public abstract void Tick();
    }
}