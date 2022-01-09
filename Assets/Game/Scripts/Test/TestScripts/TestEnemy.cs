using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public class TestEnemy : Enemy
    {
        protected override void Awake()
        {
            base.Awake();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.U))
                UseAttack();
        }
    }
}