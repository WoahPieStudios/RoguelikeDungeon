using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    [System.Flags]
    public enum TrackAction
    {
        Attack = 0,
        Skill = 1,
        Ultimate = 2
    }
}