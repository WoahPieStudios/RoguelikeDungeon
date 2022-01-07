using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;

[CreatableAsset]
public class TestPassive : PassiveEffect
{
    [SerializeField]
    ActiveEffect _ActiveEffect;

    Coroutine _TickCoroutine;

    IEnumerator Tick()
    {
        Debug.Log("Passive");

        target.AddEffects(sender, _ActiveEffect);
        yield return new WaitForSeconds(3);

        End();
    }

    public override void Stack(params Effect[] effects)
    {
        Debug.Log("passive stacked");
    }

    public override void StartEffect(CharacterBase sender, CharacterBase effected)
    {
        base.StartEffect(sender, effected);

        _TickCoroutine = target.StartCoroutine(Tick());
    }

    public override void End()
    {
        base.End();

        if(_TickCoroutine != null)
            target.StopCoroutine(_TickCoroutine);
    }
}
