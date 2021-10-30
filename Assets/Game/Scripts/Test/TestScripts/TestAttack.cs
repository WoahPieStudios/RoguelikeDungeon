using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;

[CreateAssetMenu(menuName = "Data/TestAttack")]
public class TestAttack : Attack
{
    protected override IEnumerator Tick()
    {
        Debug.Log("Attack");
        
        yield return null;

        End();
    }
}
