using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    
    [System.Flags]
    public enum RestrictAction
    {
        Movement = 0,
        Attack = 1,
        Passive = 2,
        Skill = 4,
        Ultimate = 8
    }
}