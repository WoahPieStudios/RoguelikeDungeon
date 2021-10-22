using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Animations;

namespace Game.Characters
{
    public class CharacterData<TCharacter> : ScriptableObject
        where TCharacter : Character<TCharacter>
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
        Attack<TCharacter> _Attack;
        [SerializeField]
        Sprite _Icon;

        public int maxHealth => _MaxHealth;
        
        public float moveSpeed => _MoveSpeed;

        public int attackDamage => _AttackDamage;
        public float attackRange => _AttackRange;
        public Attack<TCharacter> attack => _Attack;
        
        public Sprite icon => _Icon;
    }
}