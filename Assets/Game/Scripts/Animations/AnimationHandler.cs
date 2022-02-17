using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Animations
{
    [DisallowMultipleComponent]
    public class AnimationHandler : MonoBehaviour
    {
        private System.Action[] _CurrentAnimationEvents;
        private Animation _Animation;
        private Dictionary<string, AnimationData> _AnimationDatas = new Dictionary<string, AnimationData>();

        private struct AnimationData
        {
            public AnimationClip animationClip;
            public System.Action[] animationEvents;

            public AnimationData(AnimationClip animationClip, System.Action[] animationEvents)
            {
                this.animationClip = animationClip;
                this.animationEvents = animationEvents;
            }
        }
        
        void Awake() 
        {
            _Animation = GetComponent<Animation>();    
        }

        public AnimationState[] GetAnimationStates()
        {
            return _AnimationDatas.Select(a => _Animation[a.Key]).ToArray();
        }

        public AnimationState GetAnimationState(string name)
        {
            if(!Contains(name))
                return null;

            return _Animation[name];
        }

        public bool Contains(string name)
        {
            return _AnimationDatas.ContainsKey(name);
        }

        public bool AddAnimation(string name, AnimationClip animationClip, params System.Action[] animationEvents)
        {
            if(Contains(name))
                return false;

            _AnimationDatas.Add(name, new AnimationData(animationClip, animationEvents));

            _Animation.AddClip(animationClip, name);
            
            return true;
        }

        public bool AddAnimation(string name, AnimationClip animationClip, int layer, params System.Action[] animationEvents)
        {
            if(!AddAnimation(name, animationClip, animationEvents))
                return false;

            _Animation[name].layer = layer;

            return true;
        }

        public void RemoveAnimation(string name)
        {
            if(!Contains(name))
                return;

            AnimationClip animationClip = _AnimationDatas[name].animationClip;

            _Animation.RemoveClip(animationClip);
            _AnimationDatas.Remove(name);
        }

        public void Play(string name, PlayMode playMode)
        {
            if(!Contains(name))
                return;
                
            _CurrentAnimationEvents = _AnimationDatas[name].animationEvents;
            _Animation.Play(name, playMode);
        }

        public void Play(string name)
        {
            if(!Contains(name))
                return;

            _CurrentAnimationEvents = _AnimationDatas[name].animationEvents;
            _Animation.Play(name);
        }

        public void CrossFadePlay(string name, float fadeTime)
        {
            CrossFadePlay(name, fadeTime, PlayMode.StopSameLayer);
        }

        public void CrossFadePlay(string name, float fadeTime, PlayMode playMode)
        {
            if(!Contains(name))
                return;

            _Animation.CrossFade(name, fadeTime, playMode);
        }

        /// <summary>
        /// Syncs Animations. With bases using loop wrap mode, other animations will sync only at the first loop 
        /// </summary>
        /// <param name="baseName"></param>
        /// <param name="names"></param>
        public void SyncAnimations(string baseName, params string[] names)
        {
            if(!Contains(baseName))
                return;

            float currentTime = _Animation[baseName].time;

            foreach(string name in names)
            {
                if(!Contains(name))
                    continue;

                _Animation[name].time = currentTime;
            }
        }

        public void StopAll()
        {
            _Animation.Stop();
        }

        public void Stop(string name)
        {
            if(!Contains(name))
                return;

            _Animation.Stop(name);
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