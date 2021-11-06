using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;

[CreateAssetMenu(menuName = "Data/MagicianActive")]
public class MagicianActiveEffect : ActiveEffect
{
    int _StackCount = 0;
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
