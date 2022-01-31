using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    /// <summary>
    /// Flags Actions to be Restricted.
    /// </summary>
    [System.Flags]
    public enum RestrictActionType
    {
        None = 0,
        Movement = 1 << 1,
        Attack = 1 << 2,
        Passive = 1 << 3,
        Skill = 1 << 4,
        Ultimate = 1 << 5,
        Orientation = 1 << 5
    }
}