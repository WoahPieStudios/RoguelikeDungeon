using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters.Animations
{
    [RequireComponent(typeof(Animation))]
    public class AnimationController : MonoBehaviour
    {
        System.Action[] _CurrentAnimationEvents;

        Animation _Animation;

        Dictionary<string, AnimationData> _AnimationDatas = new Dictionary<string, AnimationData>();

        struct AnimationData
        {
            public bool isPlaying;
            public AnimationClip animationClip;
            public System.Action[] animationEvents;

            public AnimationData(AnimationClip animationClip, System.Action[] animationEvents)
            {
                this.isPlaying = false;
                this.animationClip = animationClip;
                this.animationEvents = animationEvents;
            }
        }

        void Awake() 
        {
            _Animation = GetComponent<Animation>();
        }
        
        public void AddAnimation(string name, AnimationClip animationClip, params System.Action[] animationEvents)
        {
            _AnimationDatas.Add(name, new AnimationData(animationClip, animationEvents));

            _Animation.AddClip(animationClip, name);
        }

        public void RemoveAnimation(string name)
        {
            if(!Contains(name))
                return;

            AnimationClip animationClip = _AnimationDatas[name].animationClip;

            _Animation.RemoveClip(animationClip);

            _AnimationDatas.Remove(name);
        }

        public string[] GetPlayingAnimation()
        {
            return _AnimationDatas.Where(p => p.Value.isPlaying).Select(p => p.Key).ToArray();
        }

        public void Play(string name)
        {
            if(!Contains(name))
                return;

            AnimationData animationData = _AnimationDatas[name];

            animationData.isPlaying = false;
            _CurrentAnimationEvents = animationData.animationEvents;
            _Animation.Play(name);
        }

        public bool Contains(string name)
        {
            return _AnimationDatas.ContainsKey(name);
        }

        public void Stop()
        {
            _Animation.Stop();
        }

        public void Stop(string name)
        {
            if(!Contains(name))
                return;
            
            AnimationData animationData = _AnimationDatas[name];

            if(animationData.isPlaying)
            {
                _Animation.Stop(name);
                animationData.isPlaying = false;
            }
        }

        public void CrossFadePlay(string name, float fadeTime)
        {
            _Animation.CrossFade(name, fadeTime);
        }

        public void InvokeEvent(int index)
        {
            if(index > _CurrentAnimationEvents.Length || index < 0)
            {
                Debug.LogError("Invoke Event index is out of bounds!");

                return;
            }

            _CurrentAnimationEvents[index]?.Invoke();
        }
    }
}