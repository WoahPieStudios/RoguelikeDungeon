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
        
        private AnimationPlayData GetAnimationPlayData(AnimationData animationData)
        {
            return _AnimationPlayDatas.FirstOrDefault(a => a.animationData == animationData);
        }

        private AnimationPlayData GetAnimationPlayData(int layer, bool isPlaying)
        {
            return _AnimationPlayDatas.FirstOrDefault(a => a.animationData.layer == layer && a.isPlaying && isPlaying);
        }

        private IEnumerator OnceTick(AnimationPlayData playData)
        {
            float currentTime = 0;

            AnimationData animationData = playData.animationData;
            AnimationClip animationClip = animationData.animationClip;
            
            while(currentTime <= animationClip.length)
            {
                InvokeAnimationEvents(animationData.eventTimeStamps, playData.animationEvents, animationClip.length, currentTime, Time.deltaTime);

                currentTime += Time.deltaTime;

                yield return null;
            }
        }

        private IEnumerator LoopTick(AnimationPlayData playData)
        {
            float currentTime = 0;
            
            AnimationData animationData = playData.animationData;
            AnimationClip animationClip = animationData.animationClip;

            while(playData.isPlaying)
            {
                if(currentTime > animationClip.length)
                    currentTime = 0;

                InvokeAnimationEvents(animationData.eventTimeStamps, playData.animationEvents, animationClip.length, currentTime, Time.deltaTime);

                currentTime += Time.deltaTime;

                yield return null;
            }
        }

        private IEnumerator AnimationPlayTick(AnimationPlayData playData)
        {
            playData.isPlaying = true;

            switch(playData.animationData.animationClip.wrapMode)
            {
                case WrapMode.Once:
                    yield return OnceTick(playData);
                    break;
                
                case WrapMode.Loop:
                    yield return LoopTick(playData);
                    break;
            }
                
            playData.isPlaying = false;
        }

        private void Stop(AnimationPlayData playData)
        {
            if(playData.coroutine != null)
                StopCoroutine(playData.coroutine);

            playData.isPlaying = false;

            _Animation.Stop(playData.animationData.name);
        }

        private void TryInvokeEvent(System.Action @event, float eventTime, float deltaTime)
        {
            if(eventTime >= 0 && eventTime <= deltaTime)
                @event?.Invoke();
        }

        private void InvokeAnimationEvents(EventTimeStamp[] timeStamps, System.Action[] events, float animationLength, float currentTime, float deltaTime)
        {
            float sign = Mathf.Sign(deltaTime);

            for(int i = 0; i < timeStamps.Length; i++)
            {
                if(i >= events.Length)
                    return;

                float eventTime = (timeStamps[i].timeValue * animationLength) - currentTime;

                TryInvokeEvent(events[i], sign * eventTime, sign * deltaTime);
            }
        }

        public bool IsAnimationPlaying(AnimationData animationData)
        {
            return Contains(animationData) && GetAnimationPlayData(animationData).isPlaying;
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


            AnimationPlayData previousPlayData = GetAnimationPlayData(animationData.layer, true);
            AnimationPlayData playData = GetAnimationPlayData(animationData);

            if(playData.isPlaying)
                Stop(playData);

            if(previousPlayData != null)
                Stop(previousPlayData.animationData, fadeTime);

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

        public async void Stop(AnimationData animationData, float time)
        {
            if(!Contains(animationData))
                return;

            await System.Threading.Tasks.Task.Delay(Mathf.RoundToInt(time / 1000));

            if(!gameObject && !this)
                return;

            Stop(GetAnimationPlayData(animationData));
        }
    }
}