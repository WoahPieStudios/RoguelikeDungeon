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
        GameObject _Prefab;
        [SerializeField]
        float _FadeInTime;
        [SerializeField]
        float _FadeOutTime;

        Enemy _ClosestEnemy;

        LightRay _LightRay;
        
        public int bonusDamge { get; set; }

        protected override IEnumerator Tick()
        {
            yield return null;

            Vector2 enemyDirection;

            target.FaceNearestCharacter(5);

            enemyDirection = _ClosestEnemy.transform.position - target.transform.position;

            _LightRay.position = target.transform.position + (Vector3)enemyDirection.normalized * (range / 2);
            _LightRay.rotation = Quaternion.LookRotation(Vector3.forward, enemyDirection);
            _LightRay.size = new Vector2(_LightRay.size.x, range);

            yield return null;

            _LightRay.StartLightRay(_FadeInTime, _FadeOutTime);

            _ClosestEnemy.Damage(damage);

            End();
        }

        public override bool CanUse(CharacterBase attacker)
        {
            _ClosestEnemy = Utilities.GetNearestCharacter<Enemy>(attacker.transform.position, range);

            return base.CanUse(attacker) && _ClosestEnemy;
        }

        public override void Use(CharacterBase attacker)
        {
            base.Use(attacker);

            _LightRay = Instantiate(_Prefab).GetComponent<LightRay>();
        }
    }
}