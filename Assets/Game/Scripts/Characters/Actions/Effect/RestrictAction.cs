using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    
    [System.Flags]
    public enum RestrictAction
    {
        Movement = 1,
        Attack = 2,
        Passive = 4,
        Skill = 8,
        Ultimate = 16
    }
}