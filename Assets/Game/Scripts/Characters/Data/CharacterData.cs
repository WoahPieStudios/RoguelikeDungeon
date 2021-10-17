using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Animations;

namespace Game.Characters
{
    public class CharacterData : ScriptableObject
    {
        [SerializeField]
        int _MaxHealth;
        [SerializeField]
        float _MoveSpeed;
        [SerializeField]
        int _AttackDamage;
        [SerializeField]
        float  _AttackRange;
        [SerializeField]
        Attack _Attack;
        [SerializeField]
        RuntimeAnimatorController _AnimatorController; 
        [SerializeField]
        Texture2D _Icon;

        public int maxHealth => _MaxHealth;
        
        public float moveSpeed => _MoveSpeed;

        public int attackDamage => _AttackDamage;
        public float attackRange => _AttackRange;
        public Attack attack => _Attack;

        public RuntimeAnimatorController animatorController => _AnimatorController;
        
        public Texture2D icon => _Icon; 
    }
}