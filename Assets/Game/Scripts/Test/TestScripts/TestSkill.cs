using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    [CreatableAsset]
    public class TestSkill : Skill
    {
        Coroutine _TickCoroutine;

        IEnumerator Tick()
        {
            yield return new WaitForEndOfFrame();

            End();
        }

        protected override void OnCooldown()
        {
            
        }

        public override void Use(Hero hero)
        {
            base.Use(hero);

            _TickCoroutine = target.StartCoroutine(Tick());
        }

        public override void End()
        {
            base.End();

            if(_TickCoroutine != null)
                target.StopCoroutine(_TickCoroutine);
        }
    }
}