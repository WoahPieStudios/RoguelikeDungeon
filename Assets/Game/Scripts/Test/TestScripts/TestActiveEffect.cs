using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;

[CreateAssetMenu(menuName = "Data/TestActive")]
public class TestActiveEffect : ActiveEffect
{
    public override bool isStackable => true;

    public override void Stack(Effect effect)
    {
        
    }

    public override IEnumerator Tick()
    {
        Debug.Log("Active Effect");
        yield return null;

        End();
    }
}
