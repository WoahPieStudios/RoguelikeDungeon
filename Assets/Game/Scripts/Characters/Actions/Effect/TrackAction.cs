using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    /// <summary>
    /// Flags Actions to be Tracked.
    /// </summary>
    [System.Flags]
    public enum TrackAction
    {
        None = 0,
        Attack = 1 << 1,
        Skill = 1 << 2,
        Ultimate = 1 << 3
    }
}