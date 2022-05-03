using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Items;
using Game.Parties;

namespace Game.Interactions
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class DropItem : Droppable
    {
        SpriteRenderer _SpriteRenderer;

        public IItem item { get; set; }
        public override bool canInteract => true;

        private void Awake() 
        {
            _SpriteRenderer = GetComponent<SpriteRenderer>();    
        }

        public override void OnDrop()
        {
            _SpriteRenderer.sprite = item.icon;
        }

        public override void OnInteract(Party party)
        {
            if(item == null)
                return;
                
            item.Use(party);

            gameObject.SetActive(false);
        }
    }
}