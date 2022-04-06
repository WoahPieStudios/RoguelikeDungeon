using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Upgrades;
using Game.Characters;
using Game.Characters.Properties;

public class TestUpgradeable : IUpgradeable
{
    public void Upgrade(string property, object value)
    {
        
    }
}

public class TestUpgrader : IUpgrader
{
    public void Upgrade(Hero hero)
    {
        
    }
}