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

        public override bool Use(Hero hero)
        {
            bool canUse = base.Use(hero);

            if(canUse)
                _TickCoroutine = target.StartCoroutine(Tick());

            return canUse;
        }

        public override void End()
        {
            base.End();

            if(_TickCoroutine != null)
                target.StopCoroutine(_TickCoroutine);
        }
    }
}