using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Items;
using Game.Parties;
using Game.Upgrades;
using Game.Characters.Properties;

[CreateAssetMenu(menuName = "Test Upgrade Item")]
public class TestUpgradeItem : UpgradeItem
{
    [SerializeField]
    Sprite _Icon;
    public override Sprite icon => _Icon;

    public override void Use(Party party)
    {
        party.currentHero.health.Upgrade(Health.MaxValueProperty, 5);
    }
}
