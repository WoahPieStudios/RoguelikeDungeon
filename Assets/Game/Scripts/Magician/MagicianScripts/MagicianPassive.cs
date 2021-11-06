using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;

[CreateAssetMenu(menuName = "Data/MagicianPassive")]
public class MagicianPassive : PassiveEffect
{
    [SerializeField]
    ActiveEffect _ActiveEffect;

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

    public override bool CanUse(Hero hero)
    {
        return base.CanUse(hero);
    }
}
