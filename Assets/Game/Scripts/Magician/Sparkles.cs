using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.ActiveEffects;

namespace Game.Characters.Magician
{
    [CreatableAsset("Magician")]
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
            Vector2 enemyDirection = _ClosestEnemy.transform.position - target.transform.position;

            _LightRay.position = target.transform.position + (Vector3)enemyDirection.normalized * (range / 2);
            _LightRay.rotation = Quaternion.LookRotation(Vector3.forward, enemyDirection);
            _LightRay.size = new Vector2(_LightRay.size.x, range);

            _LightRay.StartLightRay(_FadeInTime, _FadeOutTime);

        }

        public override void Use(CharacterBase attacker)
        {
            base.Use(attacker);

            target.FaceNearestCharacter(_ClosestEnemy);

            SetupLightRay();

            _ClosestEnemy.Damage(damage);
            _ClosestEnemy.AddEffects(attacker, _Knockback);

            End();
        }

        public override bool CanUse(CharacterBase attacker)
        {
            _ClosestEnemy = Utilities.GetNearestCharacter<Enemy>(attacker.transform.position, range);

            return base.CanUse(attacker) && _ClosestEnemy;
        }
    }
}