using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    [System.Flags]
    public enum TrackAction
    {
        None = 0x00,
        Attack = 0x01,
        Skill = 0x02,
        Ultimate = 0x03
    }
}