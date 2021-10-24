using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Animations;

namespace Game.Characters
{
    public class CharacterData : ScriptableObject, IIcon
    {
        [Header("Icon")]
        [SerializeField]
        Sprite _Icon;

        [Header("Health")]
        [SerializeField]
        int _MaxHealth;

        [Header("Move")]
        [SerializeField]
        float _MoveSpeed;

        [Header("Attack")]
        [SerializeField]
        Attack _Attack;

        // Health
        public int maxHealth => _MaxHealth;
        
        // Move
        public float moveSpeed => _MoveSpeed;

        // Attack
        public Attack attack => _Attack;
        
        // Icon
        public Sprite icon => _Icon;
    }
}