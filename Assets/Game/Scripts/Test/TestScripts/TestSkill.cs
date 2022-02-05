using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Actions;

namespace Game.Characters.Test
{
    public class TestSkill : Skill
    {
        Coroutine _TickCoroutine;

        IEnumerator Tick()
        {
            yield return new WaitForEndOfFrame();

            End();
        }

        protected override void Begin()
        {
            base.Begin();

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