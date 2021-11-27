using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public class TestEnemy : Enemy
    {
        [SerializeField]
        readonly EnemyData _EnemyData;
        
        protected override void Awake()
        {
            base.Awake();

            AssignData(_EnemyData);
        }
    }
}