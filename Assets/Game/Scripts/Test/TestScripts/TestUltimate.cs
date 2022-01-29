using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public class TestUltimate : Ultimate
    {
        Coroutine _TickCoroutine;

        IEnumerator Tick()
        {
            yield return new WaitForEndOfFrame();

            End();
        }

        public override void Activate(Hero hero)
        {
            base.Activate(hero);

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