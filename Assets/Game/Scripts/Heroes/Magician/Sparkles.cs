using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;
using Game.Characters.Actions;
using Game.Characters.Effects;
using Game.Characters.Animations;
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

        [Header("Transform Origin")]
        [SerializeField]
        Transform _Origin;
        [SerializeField]
        Transform _LightRayOrigin;

        [Header("Animation")]
        [SerializeField]
        AnimationClip _AnimationClip;

        Enemy _ClosestEnemy;

        Hero _Hero;

        AnimationController _AnimationController;

        public event Func<int> onUseBonusDamageEvent;

        protected override void Awake() 
        {
            base.Awake();

            _Hero = owner as Hero;

            _AnimationController = GetComponent<AnimationController>();

            _AnimationController.AddAnimation("Sparkles", _AnimationClip, SetupLightRay, End);
        }

        void SetupLightRay()
        {
            int bonusDamage = onUseBonusDamageEvent?.Invoke() ?? 0;

            Vector2 enemyDirection = _ClosestEnemy.transform.position - _LightRayOrigin.position;

            _LightRay.position = _LightRayOrigin.position + (Vector3)enemyDirection.normalized * (range / 2);
            _LightRay.rotation = Quaternion.LookRotation(Vector3.forward, enemyDirection);
            _LightRay.size = new Vector2(_LightRay.size.x, range);

            _LightRay.FadeInOutLightRay(_FadeInTime, _FadeOutTime);

            _ClosestEnemy.health.Damage(damage + bonusDamage);
            _ClosestEnemy.AddEffects(owner as Character, _Knockback);

            _Hero.mana.AddMana(manaGainOnHit);
        }

        // GOT FROM : https://forum.unity.com/threads/quaternion-lookrotation-in-2d.292572/
        Quaternion LookAt(Vector2 point)
        {
           return Quaternion.Euler(new Vector3(0f, 0f, AngleBetweenPoints(transform.position, point)));
        }
        
        float AngleBetweenPoints(Vector2 a, Vector2 b) {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }
        // END

        protected override void Begin()
        {
            base.Begin();

            _Hero.FaceNearestCharacter(_ClosestEnemy);

            _AnimationController.Play("Sparkles");

            _Origin.rotation = LookAt(_ClosestEnemy.transform.position);
        }

        public override bool Use()
        {
            _ClosestEnemy = Utilities.GetNearestCharacter<Enemy>(transform.position, range);

            return _ClosestEnemy && !_Hero.skill.isActive && !_Hero.ultimate.isActive && base.Use();
        }

        public override void End()
        {
            base.End();

            _AnimationController.Stop("Sparkles");
        }
    }
}