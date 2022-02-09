using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;
using Game.Characters.Properties;

namespace Game.FX
{
    public class DamageFlasher : MonoBehaviour
    {
        [SerializeField]
        Gradient _DamageGradient;

        Health health;

        Character _Character;
        
        SpriteRenderer _SpriteRenderer;

        Coroutine _DamageFlashCoroutine;

        void Start()
        {
            _SpriteRenderer = GetComponent<SpriteRenderer>();

            health = GetComponent<Character>().GetProperty<Health>();

            health.onDamageEvent += OnDamage;
        }

        void OnDestroy() 
        {
            health.onDamageEvent -= OnDamage;
        }

        void OnDamage(IHealthProperty health, int damage)
        {
            if(_DamageFlashCoroutine != null)
                StopCoroutine(_DamageFlashCoroutine);

            _DamageFlashCoroutine = StartCoroutine(FXUtilities.FadeOut(1, Fade));
        }

        void Fade(float value)
        {
            _SpriteRenderer.color = _DamageGradient.Evaluate(value);
        }
    }
}