using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Animations
{
    [DisallowMultipleComponent]
    public class AnimationHandler : MonoBehaviour
    {
        private Animation _Animation;
        private List<AnimationPlayData> _AnimationPlayDatas = new List<AnimationPlayData>();

        private class AnimationPlayData
        {
            public AnimationData animationData;
            public System.Action[] animationEvents;
            public Coroutine coroutine;
            public bool isPlaying;

            public AnimationPlayData(AnimationData animationData, System.Action[] animationEvents)
            {
                this.animationData = animationData;
                this.animationEvents = animationEvents;
                this.coroutine = null;
                this.isPlaying = false;
            }
        }

        private void Awake() 
        {
            _Animation = GetComponent<Animation>();    
        }

        private void Stop(AnimationPlayData playData)
        {
            if(playData.coroutine != null)
                StopCoroutine(playData.coroutine);

            playData.isPlaying = false;

            _Animation.Stop(playData.animationData.name);
        }

        private IEnumerator AnimationPlayTick(AnimationPlayData playData)
        {
            float currentTime = 0;

            AnimationData animationData = playData.animationData;

            playData.isPlaying = true;

            for(int i = 0; i < animationData.eventTimeStamps.Length; )
            {
                EventTimeStamp timeStamp = animationData.eventTimeStamps[i];

                float eventTime = animationData.animationClip.length * timeStamp.timeValue;

                if(currentTime >= eventTime)
                {
                    if(timeStamp.eventIndex >= 0 && timeStamp.eventIndex < playData.animationEvents.Length)
                        playData.animationEvents[timeStamp.eventIndex]?.Invoke();

                    i++;
                }

                else
                {
                    yield return null;

                    currentTime += Time.deltaTime;
                }
            }
                
            playData.isPlaying = false;
        }

        private AnimationPlayData GetAnimationPlayData(AnimationData animationData)
        {
            return _AnimationPlayDatas.FirstOrDefault(a => a.animationData == animationData);
        }

        public bool Contains(AnimationData animationData)
        {
            return _AnimationPlayDatas.Any(a => a.animationData == animationData);
        }

        public void AddAnimationData(AnimationData animationData, params System.Action[] animationEvents)
        {
            if(Contains(animationData))
                return;

            _AnimationPlayDatas.Add(new AnimationPlayData(animationData, animationEvents));

            _Animation.AddClip(animationData.animationClip, animationData.name);

            _Animation[animationData.name].layer = animationData.layer;
        }

        public void RemoveAnimation(AnimationData animationData)
        {
            if(!Contains(animationData))
                return;

            _Animation.RemoveClip(animationData.animationClip);

            _AnimationPlayDatas.Remove(GetAnimationPlayData(animationData));
        }

        public void Play(AnimationData animationData)
        {
            Play(animationData, PlayMode.StopSameLayer);
        }

        public void Play(AnimationData animationData, PlayMode playMode)
        {
            if(!Contains(animationData))
                return;

            AnimationPlayData playData = GetAnimationPlayData(animationData);

            if(playData.isPlaying)
                Stop(playData);

            _Animation.Play(animationData.name, playMode);

            playData.coroutine = StartCoroutine(AnimationPlayTick(playData));
        }

        public void CrossFadePlay(AnimationData animationData, float fadeTime)
        {
            CrossFadePlay(animationData, fadeTime, PlayMode.StopSameLayer);
        }

        public void CrossFadePlay(AnimationData animationData, float fadeTime, PlayMode playMode)
        {
            if(!Contains(animationData))
                return;

            AnimationPlayData playData = GetAnimationPlayData(animationData);

            if(playData.isPlaying)
                Stop(playData);

            _Animation.CrossFade(animationData.name, fadeTime, playMode);

            playData.coroutine = StartCoroutine(AnimationPlayTick(playData));
        }

        /// <summary>
        /// Syncs Animations. With bases using loop wrap mode, other animations will sync only at the first loop 
        /// </summary>
        /// <param name="baseName"></param>
        /// <param name="names"></param>
        public void SyncAnimations(AnimationData baseAnimationData, params AnimationData[] animationDatas)
        {
            if(!Contains(baseAnimationData))
                return;

            float currentTime = _Animation[baseAnimationData.name].time;

            foreach(AnimationData data in animationDatas)
            {
                if(!Contains(data))
                    continue;

                _Animation[data.name].time = currentTime;
            }
        }

        public void StopAll()
        {
            _Animation.Stop();
        }

        public void Stop(AnimationData animationData)
        {
            if(!Contains(animationData))
                return;

            Stop(GetAnimationPlayData(animationData));
        }
    }
}