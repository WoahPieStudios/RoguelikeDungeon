using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.FX
{
    public static class FXUtilities
    {
        #region Fade In
        public static IEnumerator FadeIn(float fadeInTime, Action<float> onFadeEvent)
        {
            yield return FadeIn(0, fadeInTime, onFadeEvent);
        }

        public static IEnumerator FadeIn(float fadeInTime, Action<float> onFadeEvent, Func<IEnumerator> onAfterFadeEvent)
        {
            yield return FadeIn(fadeInTime, onFadeEvent);

            yield return onAfterFadeEvent?.Invoke();
        }

        public static IEnumerator FadeIn(float fadeInTime, Action<float> onFadeEvent, Action onAfterFadeEvent)
        {
            yield return FadeIn(fadeInTime, onFadeEvent);

            onAfterFadeEvent?.Invoke();
        }

        public static IEnumerator FadeIn(float currentTime, float fadeInTime, Action<float> onFadeEvent)
        {
            while(currentTime < fadeInTime)
            {
                currentTime += Time.deltaTime;

                onFadeEvent?.Invoke(currentTime / fadeInTime);

                yield return new WaitForEndOfFrame();
            }
        }

        public static IEnumerator FadeIn(float currentTime, float fadeInTime, Action<float> onFadeEvent, Func<IEnumerator> onAfterFadeEvent)
        {
            yield return FadeIn(currentTime, fadeInTime, onFadeEvent);

            yield return onAfterFadeEvent?.Invoke();
        }

        public static IEnumerator FadeIn(float currentTime, float fadeInTime, Action<float> onFadeEvent, Action onAfterFadeEvent)
        {
            yield return FadeIn(currentTime, fadeInTime, onFadeEvent);

            onAfterFadeEvent?.Invoke();
        }
        #endregion

        #region Fade Out
        public static IEnumerator FadeOut(float fadeOutTime, Action<float> onFadeEvent)
        {
            yield return FadeOut(fadeOutTime, fadeOutTime, onFadeEvent);
        }
        
        public static IEnumerator FadeOut(float fadeOutTime, Action<float> onFadeEvent, Func<IEnumerator> onAfterFadeEvent)
        {
            yield return FadeOut(fadeOutTime, onFadeEvent);

            yield return onAfterFadeEvent?.Invoke();
        }

        public static IEnumerator FadeOut(float fadeOutTime, Action<float> onFadeEvent, Action onAfterFadeEvent)
        {
            yield return FadeOut(fadeOutTime, onFadeEvent);

            onAfterFadeEvent?.Invoke();
        }
        
        public static IEnumerator FadeOut(float currentTime, float fadeOutTime, Action<float> onFadeEvent)
        {
            while(currentTime > 0)
            {
                currentTime -= Time.deltaTime;

                onFadeEvent?.Invoke(currentTime / fadeOutTime);

                yield return new WaitForEndOfFrame();
            }
        }
        
        public static IEnumerator FadeOut(float currentTime, float fadeOutTime, Action<float> onFadeEvent, Func<IEnumerator> onAfterFadeEvent)
        {
            yield return FadeOut(currentTime, fadeOutTime, onFadeEvent);

            yield return onAfterFadeEvent?.Invoke();
        }

        public static IEnumerator FadeOut(float currentTime, float fadeOutTime, Action<float> onFadeEvent, Action onAfterFadeEvent)
        {
            yield return FadeOut(currentTime, fadeOutTime, onFadeEvent);

            onAfterFadeEvent?.Invoke();
        }
        #endregion

        public static IEnumerator FadeTransition(float fadeTime, Action<float> onFadeEvent)
        {
            yield return FadeTransition(fadeTime, fadeTime, onFadeEvent, onFadeEvent);
        }

        public static IEnumerator FadeTransition(float fadeInTime, float fadeOutTime, Action<float> onFadeEvent)
        {
            yield return FadeTransition(fadeInTime, fadeOutTime, onFadeEvent, onFadeEvent);
        }

        public static IEnumerator FadeTransition(float fadeInTime, float fadeOutTime, Action<float> onFadeInEvent, Action<float> onFadeOutEvent)
        {
            yield return FadeIn(fadeInTime, onFadeInEvent);

            yield return FadeOut(fadeOutTime, onFadeOutEvent);
        }

        public static IEnumerator FadeTransition(float fadeInTime, float fadeOutTime, Action<float> onFadeInEvent, Action<float> onFadeOutEvent, Func<IEnumerator> onAfterFadeInEvent, Action onAfterFadeOutEvent)
        {
            yield return FadeIn(fadeInTime, onFadeInEvent, onAfterFadeInEvent);

            yield return FadeOut(fadeOutTime, onFadeOutEvent, onAfterFadeOutEvent);
        }
        
        public static IEnumerator FadeTransition(float fadeInTime, float fadeOutTime, Action<float> onFadeInEvent, Action<float> onFadeOutEvent, Action onAfterFadeInEvent, Func<IEnumerator> onAfterFadeOutEvent)
        {
            yield return FadeIn(fadeInTime, onFadeInEvent, onAfterFadeInEvent);

            yield return FadeOut(fadeOutTime, onFadeOutEvent, onAfterFadeOutEvent);
        }

        public static IEnumerator FadeTransition(float fadeInTime, float fadeOutTime, Action<float> onFadeInEvent, Action<float> onFadeOutEvent, Action onAfterFadeInEvent, Action onAfterFadeOutEvent)
        {
            yield return FadeIn(fadeInTime, onFadeInEvent, onAfterFadeInEvent);

            yield return FadeOut(fadeOutTime, onFadeOutEvent, onAfterFadeOutEvent);
        }
        
        public static IEnumerator FadeTransition(float fadeInTime, float fadeOutTime, Action<float> onFadeInEvent, Action<float> onFadeOutEvent, Func<IEnumerator> onAfterFadeInEvent, Func<IEnumerator> onAfterFadeOutEvent)
        {
            yield return FadeIn(fadeInTime, onFadeInEvent, onAfterFadeInEvent);

            yield return FadeOut(fadeOutTime, onFadeOutEvent, onAfterFadeOutEvent);
        }
    }
}