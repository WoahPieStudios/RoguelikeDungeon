using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;

[CreateAssetMenu(menuName = "Data/TestActive")]
public class TestActiveEffect : ActiveEffect
{
    [SerializeField]
    RestrictAction _RestictAction;
    int _StackCount = 0;
    public override bool isStackable => false;

    public override RestrictAction restrictAction => _RestictAction;

    protected override IEnumerator Tick()
    {
        Debug.Log("Active Effect");
        yield return new WaitForSeconds(5);
        Debug.Log("End Active Effect");

        End();
    }

    public override void Stack(params Effect[] effects)
    {
        _StackCount += effects.Length;
        Debug.Log(effects.Length);
    }
}
