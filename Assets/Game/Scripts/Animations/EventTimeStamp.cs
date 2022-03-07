using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Animations
{
    [System.Serializable]
    public struct EventTimeStamp
    {
        public EventTimeType timeType;
        public float timeValue;
        public int eventIndex;
    }
}