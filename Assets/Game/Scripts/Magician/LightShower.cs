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

        ParticleSystem _InstantiatedLightShowerParticles;

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
                        character.Damage(_Damage);
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

        public override void Activate(Hero hero)
        {
            base.Activate(hero);

            _TickCoroutine = StartCoroutine(Tick());

            _InstantiatedLightShowerParticles = Instantiate(_LightShowerParticles, _ClosestEnemy.transform.position + _Offset, Quaternion.identity);
        }

        public override void End()
        {
            base.End();

            if(_TickCoroutine != null)
                StopCoroutine(_TickCoroutine);

            Destroy(_InstantiatedLightShowerParticles.gameObject);
        }
    }
}
