using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Parties;

namespace Game.Items
{
    public interface IItem
    {
        int quantity { get; }
        void Use(Party party);
    }
}
