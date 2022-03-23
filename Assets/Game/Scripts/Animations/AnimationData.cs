using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Animations
{
    [CreateAssetMenu(menuName = "Animations/Animation Data")]
    public class AnimationData : ScriptableObject
    {
        [SerializeField]
        AnimationClip _AnimationClip;
        [SerializeField]
        int _Layer;
        [SerializeField]
        EventTimeStamp[] _EventTimeStamps;

        public AnimationClip animationClip => _AnimationClip;
        public int layer => _Layer;
        public EventTimeStamp[] eventTimeStamps => _EventTimeStamps;
    }
}