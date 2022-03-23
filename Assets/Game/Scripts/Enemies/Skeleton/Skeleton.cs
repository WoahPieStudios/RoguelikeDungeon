using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Actions;
using Game.Characters;

namespace Game.Enemies.Skeleton
{
    public class Skeleton : Character
    {
        [SerializeField]
        private float _InputLerpTime;

        private Vector2 _InputAxis;

        private IAttackAction _Attack;

        protected override void Awake()
        {
            base.Awake();

            _Attack = GetComponent<IAttackAction>();
        }

        private void Update() 
        {
            Vector2 inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

            _InputAxis = Vector2.Lerp(_InputAxis, inputAxis, _InputLerpTime);

            movement.Move(_InputAxis);

            // orientation.Orientate(Vector2Int.RoundToInt(inputAxis));

            if(Input.GetKeyDown(KeyCode.J))
                _Attack.Use();
        }
    }
}