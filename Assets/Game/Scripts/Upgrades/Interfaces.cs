using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Properties;

namespace Game.Upgrades
{
    public interface IUpgradeable : IPropertyHandler
    {
        void Revert(string property);
        void Upgrade(string property, float value);
    }
}