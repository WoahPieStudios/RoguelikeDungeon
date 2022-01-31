using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;
using Game.Characters.Actions;

namespace Game.Heroes.Magician
{
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

                    foreach(Enemy enemy in Utilities.GetCharacters<Enemy>(aoePosition, _Radius, owner as Character))
                        enemy.health.Damage(_Damage);
                }

                yield return null;
            }

            End();
        }

        public override bool CanUse()
        {

            _ClosestEnemy = Utilities.GetNearestCharacter<Enemy>(transform.position, _FindingRange);

            return base.CanUse() && _ClosestEnemy;
        }

        public override bool Use()
        {
            bool canUse = base.Use();

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
