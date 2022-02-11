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
        int _Damage;
        [SerializeField]
        float _Radius;
        [SerializeField]
        float _FieldTime;
        [SerializeField]
        int _DamageInterval;

        [Header("Targetting")]
        [SerializeField]
        float _FindingRange;

        [Header("VFX")]
        [SerializeField]
        ParticleSystem _LightShowerParticles;
        [SerializeField]
        Vector3 _Offset;

        Enemy _ClosestEnemy;

        Coroutine _TickCoroutine;

        Hero _Hero;

        protected override void Awake()
        {
            base.Awake();

            _Hero = owner as Hero;
        }

        IEnumerator Tick()
        {
            Vector3 aoePosition = _ClosestEnemy.transform.position;

            float currentTime = 0;
            float currentInterval = 0;
            float timePerInterval = _FieldTime / (_DamageInterval + 2);

            while(currentTime < _FieldTime)
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

        protected override void Begin()
        {
            base.Begin();

            _TickCoroutine = StartCoroutine(Tick());

            _LightShowerParticles.transform.SetParent(null, false);
            _LightShowerParticles.transform.position = _ClosestEnemy.transform.position + _Offset;
            _LightShowerParticles.Play();
        }

        public override bool Use()
        {
            _ClosestEnemy = Utilities.GetNearestCharacter<Enemy>(transform.position, _FindingRange);

            bool canUse = _ClosestEnemy && !_Hero.skill.isActive && base.Use();

            if(canUse && _Hero.attack.isActive)
                _Hero.attack.End();

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
