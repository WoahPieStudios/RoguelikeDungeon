using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;

[CreateAssetMenu(menuName = "Data/TestPassive")]
public class TestPassive : PassiveEffect
{
    [SerializeField]
    TrackAction _TrackAction;
    [SerializeField]
    ActiveEffect _ActiveEffect;

    public override bool isStackable => false;

    public override TrackAction trackAction => _TrackAction;

    protected override IEnumerator Tick()
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
}
