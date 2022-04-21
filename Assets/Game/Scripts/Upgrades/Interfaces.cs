using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Properties;

namespace Game.Upgrades
{
    public interface IUpgradeable : IPropertyHandler
    {
        
    }

    public interface IUpgradeHandler
    {
        void Upgrade(IUpgradeable upgradeable, string property, float value);
        void Revert(IUpgradeable upgradeable, string property);
    }
}