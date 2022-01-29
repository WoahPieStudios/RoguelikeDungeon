using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;
using Game.Characters.Interfaces;

[CreatableAsset]
public class TestActiveEffect : ActiveEffect, IStackableEffect
{
    [SerializeField]
    bool _IsStackable;
    int _StackCount = 0;

    Coroutine _TickCoroutine;

    public bool isStackable => _IsStackable;

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
