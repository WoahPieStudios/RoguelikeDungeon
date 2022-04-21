using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Upgrades;
using Game.Properties;

namespace Game.Characters.Actions
{
    public abstract class HeroMovement<T> : Movement<T>, IHeroMovementAction where T : Hero
    {
        
    }
}