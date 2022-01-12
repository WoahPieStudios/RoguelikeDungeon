using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;
using Game.Characters.Interfaces;
using Game.FX;

public class DamageFlasher : MonoBehaviour
{
    [SerializeField]
    Gradient _DamageGradient;
    CharacterBase _Character;
    
    SpriteRenderer _SpriteRenderer;

    Coroutine _DamageFlashCoroutine;

    void Start()
    {
        _SpriteRenderer = GetComponent<SpriteRenderer>();

        _Character = GetComponent<CharacterBase>();

        _Character.onDamageEvent += OnDamage;
    }

    void OnDestroy() 
    {
        _Character.onDamageEvent -= OnDamage;
    }

    void OnDamage(IHealth health, int damage)
    {
        if(_DamageFlashCoroutine != null)
            StopCoroutine(_DamageFlashCoroutine);

        Debug.Log("Damage");

        _DamageFlashCoroutine = StartCoroutine(FXUtilities.FadeOut(1, Fade));
    }

    void Fade(float value)
    {
        _SpriteRenderer.color = _DamageGradient.Evaluate(value);
    }
}
