using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Actions;

namespace Game.Characters.Test
{
    public class TestSkill : Skill<Hero>
    {
        Coroutine _TickCoroutine;

        IEnumerator Tick()
        {
            yield return new WaitForSeconds(5);

            End();
        }

        protected override void OnUse()
        {
            base.OnUse();

            _TickCoroutine = StartCoroutine(Tick());
        }

        public override void End()
        {
            base.End();

            if(_TickCoroutine != null)
                StopCoroutine(_TickCoroutine);
        }
    }
}