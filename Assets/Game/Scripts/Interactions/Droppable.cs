using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Parties;

namespace Game.Interactions
{
    public abstract class Droppable : Interactable, IDroppable
    {
        public abstract void OnDrop();
    }
}