using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Upgrades
{
    public interface IUpgradeable
    {
        
    }

    public interface IUpgrader
    {
        void Upgrade(IUpgradeable upgradeable);
    }
}