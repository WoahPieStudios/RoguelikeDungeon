using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Upgrades
{
    public static class Utilities
    {
        public static bool DebugAssertProperty(IUpgradeable upgradeable, string property)
        {
            if(!upgradeable.ContainsProperty(property))
            {
                Debug.LogAssertion($"[Movement Upgrade Error] {property} does not exist in {upgradeable.GetType()}!");
                
                return false;
            }

            return true;
        }
    }
}