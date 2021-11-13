using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;

[CreateAssetMenu(menuName = "Data/TestPassive")]
public class TestPassive : PassiveEffect, IAttackBonus
{

    public int damageBonus => 5;

    public float rangeBonus => 5;

    public float speedBonus => 5;

    protected override IEnumerator Tick()
    {
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

    public override void StartEffect(CharacterBase sender, CharacterBase effected)
    {
        base.StartEffect(sender, effected);

        if(target.attack is IReceiveAttackBonus)
        {
            (target.attack as IReceiveAttackBonus).AddAttackBonus(this);
        }

    }

    public override void End()
    {
        if(target.attack is IReceiveAttackBonus)
        {
            (target.attack as IReceiveAttackBonus).RemoveAttackBonus(this);
        }

        base.End();
    }
}
