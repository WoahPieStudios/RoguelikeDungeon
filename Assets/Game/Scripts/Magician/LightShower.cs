using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    [CreatableAsset("Magician")]
    public class LightShower : Ultimate
    {
        [SerializeField]
        float _FindingRange;

        [SerializeField]
        ParticleSystem _LightShowerParticles;
        [SerializeField]
        Vector3 _Offset;
        [SerializeField]
        float _Time;
        [SerializeField]
        int _Damage;
        [SerializeField]
        int _DamageInterval;
        [SerializeField]
        float _Radius;

        Enemy _ClosestEnemy;

        Coroutine _TickCoroutine;

        IEnumerator Tick()
        {
            Vector3 aoePosition = _ClosestEnemy.transform.position;

            float currentTime = 0;
            float currentInterval = 0;
            float timePerInterval = _Time / (_DamageInterval + 2);

            while(currentTime < _Time)
            {
                currentTime += Time.deltaTime;

                if(Mathf.FloorToInt(currentTime / timePerInterval) > currentInterval && currentInterval < _DamageInterval)
                {
                    currentInterval++;

                    foreach(CharacterBase character in Utilities.GetCharacters<Enemy>(aoePosition, _Radius, target))
                        character.health.Damage(_Damage);
                }

                yield return null;
            }

            End();
        }

        public override bool CanUse(Hero hero)
        {
            _ClosestEnemy = Utilities.GetNearestCharacter<Enemy>(hero.transform.position, _FindingRange);

            return base.CanUse(hero) && _ClosestEnemy;
        }

        public override bool Use(Hero hero)
        {
            bool canUse = base.Use(hero);

            if(canUse)
            {
                _TickCoroutine = StartCoroutine(Tick());

                _LightShowerParticles.transform.SetParent(null, false);
                _LightShowerParticles.transform.position = _ClosestEnemy.transform.position + _Offset;
                _LightShowerParticles.Play();
            }

            return canUse;
        }

        public override void End()
        {
            base.End();

            if(_TickCoroutine != null)
                StopCoroutine(_TickCoroutine);

            _LightShowerParticles.Stop();
            _LightShowerParticles.transform.SetParent(transform, false);

            // Destroy(_InstantiatedLightShowerParticles.gameObject);
        }
    }
}
