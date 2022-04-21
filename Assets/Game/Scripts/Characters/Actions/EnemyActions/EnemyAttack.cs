using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Properties;
using Game.Actions;

namespace Game.Characters.Actions
{
    public abstract class EnemyAttack<T> : Attack<T>, IAttackAction where T : Enemy
    {
        
    }
}