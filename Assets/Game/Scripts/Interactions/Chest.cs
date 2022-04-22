using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Items;
using Game.Parties;

namespace Game.Interactions
{
    public class Chest : Interactable
    {
        bool _CanInteract = true;

        public override bool canInteract => _CanInteract;

        public IItem[] items { get; set; }

        public override void OnInteract(Party party)
        {
            _CanInteract = false;

            if(items == null)
                return;

            for(int i = 0; i < items.Length; i++)
                items[i].Use(party);
        }
    }
}