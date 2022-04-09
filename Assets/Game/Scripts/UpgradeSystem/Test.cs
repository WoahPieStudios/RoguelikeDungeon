using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Upgrades;
using Game.Characters;
using Game.Characters.Properties;

public class TestUpgradeable : IUpgradeable
{
    public bool Contains(string property)
    {
        throw new System.NotImplementedException();
    }

    public object GetStartValue(string property)
    {
        throw new System.NotImplementedException();
    }

    public void Revert(string property)
    {
        throw new System.NotImplementedException();
    }

    public void Upgrade(string property, object value)
    {
        
    }
}

public class TestUpgrader : IUpgrader<Hero>
{
    public void Revert(Hero character)
    {
        
    }

    public void Upgrade(Hero character)
    {
        character.attack.Upgrade("speed", character.attack.speed + character.attack.speed * 0.1f);
    }
}