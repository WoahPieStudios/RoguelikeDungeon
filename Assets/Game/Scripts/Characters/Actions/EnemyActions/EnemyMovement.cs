using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Properties;

namespace Game.Characters.Actions
{
    public abstract class EnemyMovement<T> : Movement<T>, IMovementAction where T : Enemy
    {
        
    }
}