using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    [System.Flags]
    public enum TrackAction
    {
        Movement = 0,
        Attack = 1,
        Skill = 2,
        Ultimate = 4
    }
}