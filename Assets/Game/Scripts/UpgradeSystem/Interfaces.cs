using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;

namespace Game.Upgrades
{
    public interface IUpgradeable
    {
        object GetStartValue(string property);
        void Revert(string property);
        bool Contains(string property);
        void Upgrade(string property, object value);
    }

    public interface IUpgrader<T> where T : Character
    {
        void Upgrade(T character);
        void Revert(T character);
    }
}