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

        void Awake() 
        {
            _SpriteRenderer = GetComponent<SpriteRenderer>();

            _SpriteRenderer.color = _Gradient.Evaluate(0);
        }

        IEnumerator Sequence(float fadeIn, float fadeOut)
        {
            yield return FXUtilities.FadeTransition(fadeIn, fadeOut, LaserFadeOut);

            Destroy(gameObject);
        }

        void LaserFadeOut(float value)
        {
            _SpriteRenderer.color = _Gradient.Evaluate(value);
        }

        public void StartLightRay(float fadeIn, float fadeOut)
        {
            StartCoroutine(Sequence(fadeIn, fadeOut));
        }
    }
}