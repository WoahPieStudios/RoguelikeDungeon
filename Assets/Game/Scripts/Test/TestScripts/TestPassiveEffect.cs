using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Effects;

namespace Game.Characters.Test
{
    public class TestPassiveEffect : PassiveEffect, IStackableEffect
    {
        [SerializeField]
        ActiveEffect _ActiveEffect;

        Coroutine _TickCoroutine;

        IEnumerator Tick()
        {
            Debug.Log("Passive");

            sender.AddEffects(sender, _ActiveEffect);
            yield return new WaitForSeconds(3);

            End();
        }

        public void Stack(params Effect[] effects)
        {
            Debug.Log("passive stacked");
        }

        public override void StartEffect(IEffectable sender, IEffectable receiver)
        {
            base.StartEffect(sender, receiver);

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