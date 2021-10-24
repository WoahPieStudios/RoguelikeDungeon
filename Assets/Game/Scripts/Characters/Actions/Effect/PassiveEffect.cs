using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class PassiveEffect : Effect
    {
        [SerializeField]
        TrackAction _TrackAction;

        public TrackAction trackAction => _TrackAction;

        public virtual void Initialize(CharacterBase effected)
        {
            Begin(effected);
        }
    }
}