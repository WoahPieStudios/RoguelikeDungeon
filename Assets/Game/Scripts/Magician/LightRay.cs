using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.FX;

namespace Game.Characters.Magician
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class LightRay : MonoBehaviour
    {
        [SerializeField]
        Gradient _Gradient;

        SpriteRenderer _SpriteRenderer;

        public Vector2 size { get => _SpriteRenderer.size; set => _SpriteRenderer.size = value; }

        public Vector3 position { get => transform.position; set => transform.position = value; }
        public Quaternion rotation { get => transform.rotation; set => transform.rotation = value; }

        Coroutine _FadeCoroutine;

        void Awake() 
        {
            _SpriteRenderer = GetComponent<SpriteRenderer>();

            _SpriteRenderer.color = _Gradient.Evaluate(0);
        }

        void LaserFadeOut(float value)
        {
            _SpriteRenderer.color = _Gradient.Evaluate(value);
        }

        public void FadeInLightRay(float fadeTime)
        {
            if(_FadeCoroutine != null)
                StopCoroutine(_FadeCoroutine);

            _FadeCoroutine = StartCoroutine(FXUtilities.FadeIn(fadeTime, LaserFadeOut));
        }

        public void FadeOutLightRay(float fadeTime)
        {
            if(_FadeCoroutine != null)
                StopCoroutine(_FadeCoroutine);

            _FadeCoroutine = StartCoroutine(FXUtilities.FadeOut(fadeTime, LaserFadeOut));
        }

        public void FadeInOutLightRay(float fadeIn, float fadeOut)
        {
            if(_FadeCoroutine != null)
                StopCoroutine(_FadeCoroutine);

            _FadeCoroutine = StartCoroutine(FXUtilities.FadeTransition(fadeIn, fadeOut, LaserFadeOut));
        }
    }
}