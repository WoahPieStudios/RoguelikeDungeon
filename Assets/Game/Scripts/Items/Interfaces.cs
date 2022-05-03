using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Parties;
using Game.Characters;

namespace Game.Items
{
    public interface IItem : IIcon
    {
        int quantity { get; set; }
        void Use(Party party);
    }
}
