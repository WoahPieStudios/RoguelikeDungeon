using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Actions;

namespace Game.Characters
{
    public class Enemy : Character
    {
        Attack<Enemy> _Attack;

        public Attack<Enemy> attack => _Attack;

        protected override void Awake()
        {
            base.Awake();

            _Attack = GetComponent<Attack<Enemy>>();
        }
    }
}