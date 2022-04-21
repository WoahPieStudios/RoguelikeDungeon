using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Items;
using Game.Parties;
using Game.Upgrades;

public class TestUpgradeItem : UpgradeItem
{
    public override void Use(Party party)
    {
        party.currentHero.Upgrade(party.currentHero.health, Game.Characters.Properties.Health.maxHealthProperty, 5);
    }
}
