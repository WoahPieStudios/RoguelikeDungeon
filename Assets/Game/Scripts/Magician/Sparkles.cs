using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters.Magician
{
    [CreatableAsset("Magician")]
    public class Sparkles : Attack, IBonusDamage
    {
        [Header("Light Ray")]
        [SerializeField]
        LightRay _LightRay;
        [SerializeField]
        float _FadeInTime;
        [SerializeField]
        float _FadeOutTime;

        Enemy _ClosestEnemy;

        public int bonusDamge { get; set; }

        // IEnumerator Tick()
        // {
        //     yield return null;

        //     Vector2 enemyDirection;

        //     target.FaceNearestCharacter(_ClosestEnemy);

        //     enemyDirection = _ClosestEnemy.transform.position - target.transform.position;

        //     _LightRay.position = target.transform.position + (Vector3)enemyDirection.normalized * (range / 2);
        //     _LightRay.rotation = Quaternion.LookRotation(Vector3.forward, enemyDirection);
        //     _LightRay.size = new Vector2(_LightRay.size.x, range);

        //     yield return null;

        //     _LightRay.StartLightRay(_FadeInTime, _FadeOutTime);

        //     _ClosestEnemy.Damage(damage);

        //     End();
        // }

        public override void Use(CharacterBase attacker)
        {
            base.Use(attacker);

            Vector2 enemyDirection;

            target.FaceNearestCharacter(_ClosestEnemy);

            enemyDirection = _ClosestEnemy.transform.position - target.transform.position;

            _LightRay.position = target.transform.position + (Vector3)enemyDirection.normalized * (range / 2);
            _LightRay.rotation = Quaternion.LookRotation(Vector3.forward, enemyDirection);
            _LightRay.size = new Vector2(_LightRay.size.x, range);

            _LightRay.StartLightRay(_FadeInTime, _FadeOutTime);

            _ClosestEnemy.Damage(damage);

            End();
        }

        public override bool CanUse(CharacterBase attacker)
        {
            _ClosestEnemy = Utilities.GetNearestCharacter<Enemy>(attacker.transform.position, range);

            Debug.Log(_ClosestEnemy);

            return base.CanUse(attacker) && _ClosestEnemy;
        }
    }
}