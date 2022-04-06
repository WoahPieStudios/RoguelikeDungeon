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

public class TestUpgrader : IUpgrader<Hero>
{
    public void Upgrade(Hero character)
    {
        character.attack.Upgrade("speed", character.attack.speed + character.attack.speed * 0.1f);
    }
}