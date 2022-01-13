using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters.Magician
{
    [CreatableAsset("Magician")]
    public class Radiance : Skill
    {
        [Header("Targetting")]
        [SerializeField]
        float _FindingRange;
        [SerializeField]
        float _AoeRange;
        [SerializeField]
        int _Damage;
        
        [Header("Light Ray")]
        [SerializeField]
        LightRay _LightRay;
        [SerializeField]
        float _FadeInTime;
        [SerializeField]
        float _FadeOutTime;

        Enemy _ClosestEnemy;

        Vector2 _LightRayOrigSize;

        Coroutine _TickCoroutine;

        IEnumerator Tick()
        {
            _LightRay.FadeInLightRay(_FadeInTime);
            _LightRay.transform.position = _ClosestEnemy.transform.position;
                
            yield return new WaitForSeconds(_FadeInTime);

            foreach(Enemy enemy in Utilities.GetCharacters<Enemy>(_ClosestEnemy.transform.position, _AoeRange, target))
                enemy.Damage(_Damage);
                
            yield return new WaitForSeconds(_FadeOutTime);
            
            _LightRay.FadeOutLightRay(_FadeOutTime);

            End();
        }

        public override bool CanUse(Hero hero)
        {
            _ClosestEnemy = Utilities.GetNearestCharacter<Enemy>(hero.transform.position, _FindingRange);

            return base.CanUse(hero) && _ClosestEnemy;
        }

        public override void Use(Hero hero)
        {
            base.Use(hero);

            _LightRayOrigSize = _LightRay.size;

            _TickCoroutine = StartCoroutine(Tick());
        }

        public override void End()
        {
            base.End();

            if(_TickCoroutine != null)
                StopCoroutine(_TickCoroutine);

            _LightRay.size = _LightRayOrigSize;
        }
    }
}
