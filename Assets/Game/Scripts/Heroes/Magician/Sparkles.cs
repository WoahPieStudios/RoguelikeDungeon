using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;
using Game.Characters.Actions;
using Game.Characters.Effects;

namespace Game.Heroes.Magician
{
    public class Sparkles : Attack, IBonusDamage
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

        public int bonusDamge { get; set; }

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
            bool canUse = base.Use();

            if(canUse)
            {
                (owner as Character).FaceNearestCharacter(_ClosestEnemy);

                SetupLightRay();

                _ClosestEnemy.health.Damage(damage);
                _ClosestEnemy.AddEffects(owner as Character, _Knockback);
                End();
            }

            return canUse;
        }

        public override bool CanUse()
        {
            _ClosestEnemy = Utilities.GetNearestCharacter<Enemy>(transform.position, range);

            return base.CanUse() && _ClosestEnemy;
        }
    }
}