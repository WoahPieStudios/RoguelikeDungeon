using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class Passive : Action
    {
        [SerializeField]
        TrackAction _TrackAction;

        public TrackAction trackAction => _TrackAction;
    }
}