using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;
using Game.Characters.Actions;
using Game.Characters.Effects;
using System;

namespace Game.Heroes.Magician
{
    public class Sparkles : Attack, IGlintBonusDamage
    {
        [SerializeField]
        Knockback _Knockback;
        [Header("Light Ray")]
        [SerializeField]
        LightRay _LightRay;
        [SerializeField]
        float _FadeInTime;
        [SerializeField]
        float _FadeOutTime;

        Enemy _ClosestEnemy;

        Hero _Hero;

        public event Func<int> onUseBonusDamageEvent;

        protected override void Awake() 
        {
            base.Awake();

            _Hero = owner as Hero;
        }

        void SetupLightRay()
        {
            Vector2 enemyDirection = _ClosestEnemy.transform.position - transform.position;

            _LightRay.position = transform.position + (Vector3)enemyDirection.normalized * (range / 2);
            _LightRay.rotation = Quaternion.LookRotation(Vector3.forward, enemyDirection);
            _LightRay.size = new Vector2(_LightRay.size.x, range);

            _LightRay.FadeInOutLightRay(_FadeInTime, _FadeOutTime);

        }

        public override bool Use()
        {
            _ClosestEnemy = Utilities.GetNearestCharacter<Enemy>(transform.position, range);

            bool canUse = base.Use() && _ClosestEnemy;

            if(canUse)
            {
                int bonusDamage = onUseBonusDamageEvent?.Invoke() ?? 0;

                _Hero.FaceNearestCharacter(_ClosestEnemy);

                SetupLightRay();

                _ClosestEnemy.health.Damage(damage + bonusDamage);
                _ClosestEnemy.AddEffects(owner as Character, _Knockback);

                _Hero.mana.AddMana(manaGainOnHit);

                End();
            }

            return canUse;
        }
    }
}