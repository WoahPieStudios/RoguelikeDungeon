using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class Passive<TCharacter> : Action<Passive<TCharacter>>, IStackable
        where TCharacter : Character<TCharacter>
    {
        [SerializeField]
        TrackAction _TrackAction;
        [SerializeField]
        Effect<TCharacter>[] _Effects;

        TCharacter _Effected;

        public abstract bool isStackable { get; }
        public TrackAction trackAction => _TrackAction;
        public Effect<TCharacter>[] effects => _Effects;
        public TCharacter effected => _Effected;

        public virtual void Initialize(TCharacter effected)
        {
            _Effected = effected;

            // Add Effect to Character
        }

        public override void End()
        {
            base.End();

            // Remove Effect to Character
        }
    }
}