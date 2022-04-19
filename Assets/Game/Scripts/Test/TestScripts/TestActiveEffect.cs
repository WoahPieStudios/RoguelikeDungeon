using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Effects;
using Game.Characters.Effects;

namespace Game.Characters.Test
{
    public class TestActiveEffect : ActiveEffect, IStackableEffect
    {
        int _StackCount = 0;

        Coroutine _TickCoroutine;

        IEnumerator Tick()
        {
            Debug.Log("Active Effect");
            yield return new WaitForSeconds(5);
            Debug.Log("End Active Effect");

            End();
        }

        public void Stack(params Effect[] effects)
        {
            _StackCount += effects.Length;

            if(_StackCount > 3)
                Debug.LogWarning("test active stacked!");
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