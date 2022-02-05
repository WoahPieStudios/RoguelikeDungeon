using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;
using Game.Characters.Actions;

namespace Game.Heroes.Magician
{
    public class Radiance : Skill
    {
        [SerializeField]
        float _AoeRange;
        [SerializeField]
        int _Damage;
        [SerializeField]
        int _ManaGainOnHit;

        [Header("Targetting")]
        [SerializeField]
        float _FindingRange;

        [Header("Sun Ray")]
        [SerializeField]
        SpriteRenderer _SunRay;
        [SerializeField]
        Gradient _FadeGradient;
        [SerializeField]
        float _SunRayFadeInTime;
        [SerializeField]
        float _SunRayFadeOutTime;
        
        [Header("Light Ray")]
        [SerializeField]
        LightRay _LightRay;
        [SerializeField]
        float _FadeInTime;
        [SerializeField]
        float _FadeOutTime;

        Enemy _ClosestEnemy;

        Coroutine _TickCoroutine;

        void SunRayFade(float value)
        {
            _SunRay.color = _FadeGradient.Evaluate(value);
        }

        IEnumerator Tick()
        {
            yield return FX.FXUtilities.FadeIn(_SunRayFadeInTime, SunRayFade);

            _LightRay.FadeInLightRay(_FadeInTime);
                
            yield return new WaitForSeconds(_FadeInTime);

            Enemy[] enemies = Utilities.GetCharacters<Enemy>(_ClosestEnemy.transform.position, _AoeRange, owner as Character);

            if(enemies.Length > 0)
            {
                (owner as Hero).mana.AddMana(_ManaGainOnHit);

                foreach(Enemy enemy in enemies)
                    enemy.health.Damage(_Damage);
            }
                
            yield return new WaitForSeconds(_FadeOutTime);
            
            _LightRay.FadeOutLightRay(_FadeOutTime);

            yield return FX.FXUtilities.FadeOut(_SunRayFadeOutTime, SunRayFade);

            End();
        }

        public override bool Use()
        {
            _ClosestEnemy = Utilities.GetNearestCharacter<Enemy>(transform.position, _FindingRange);

            bool canUse = base.Use() && _ClosestEnemy;

            if(canUse)
            {
                _LightRay.transform.SetParent(null, false);
                _LightRay.transform.position = _ClosestEnemy.transform.position;

                _TickCoroutine = StartCoroutine(Tick());

                _SunRay.transform.SetParent(null, false);
                _SunRay.transform.position = _ClosestEnemy.transform.position;
            }

            return canUse;
        }

        public override void End()
        {
            base.End();

            if(_TickCoroutine != null)
                StopCoroutine(_TickCoroutine);

            _SunRay.color = _FadeGradient.Evaluate(0);
            _SunRay.transform.SetParent(transform, false);

            
            _LightRay.transform.SetParent(transform, false);
        }
    }
}
