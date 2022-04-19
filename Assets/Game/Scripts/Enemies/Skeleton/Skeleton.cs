using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Actions;
using Game.Characters;
using Game.Properties;

namespace Game.Enemies.Skeleton
{
    public class Skeleton : Enemy
    {
        [SerializeField]
        private float _InputLerpTime;

        private Vector2 _InputAxis;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Update() 
        {
            Vector2 inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

            _InputAxis = Vector2.Lerp(_InputAxis, inputAxis, _InputLerpTime);

            movement.Move(_InputAxis);

            // orientation.Orientate(Vector2Int.RoundToInt(inputAxis));

            if(Input.GetKeyDown(KeyCode.J))
                attack.Use();
        }
    }
}