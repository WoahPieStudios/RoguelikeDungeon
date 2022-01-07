using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public class TestEnemy : Enemy
    {
        [SerializeField]
        EnemyData _EnemyData;
        
        protected override void Awake()
        {
            base.Awake();

            AssignData(_EnemyData);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.U))
                UseAttack();
        }
    }
}