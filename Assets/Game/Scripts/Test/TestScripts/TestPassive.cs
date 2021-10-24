using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;

[CreateAssetMenu(menuName = "Data/TestPassive")]
public class TestPassive : PassiveEffect
{
    [SerializeField]
    ActiveEffect _ActiveEffect;

    public override bool isStackable => true;

    public override void Stack(Effect effect)
    {

    }

    public override IEnumerator Tick()
    {
        Debug.Log("Passive");

        characterBase.AddEffects(_ActiveEffect);
        yield return null;

        End();
    }
}
