using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Items;
using Game.Parties;

namespace Game.Upgrades
{
    public abstract class UpgradeItem : ScriptableObject, IItem
    {
        public int quantity { get; set; }

        public abstract void Use(Party party);
    }
}