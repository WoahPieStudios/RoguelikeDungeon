using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Properties;

namespace Game.Upgrades
{
    public interface IUpgradeable : IPropertyHandler
    {
        event Action<IProperty, float> onUpgradePropertyEvent;
        event Action<IProperty> onRevertPropertyEvent;
        void Upgrade(string property, float value);
        void Revert(string property);
    }
}