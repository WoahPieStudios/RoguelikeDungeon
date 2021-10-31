using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    
    [System.Flags]
    public enum RestrictAction
    {
        None = 0x00,
        Movement = 0x01,
        Attack = 0x02,
        Passive = 0x03,
        Skill = 0x04,
        Ultimate = 0x05
    }
}