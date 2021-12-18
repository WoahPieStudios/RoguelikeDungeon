using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Animations;

namespace Game.Characters
{
    [RootCreatableAsset]
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
        /// <summary>
        /// Max Health of the Character.
        /// </summary>
        public int maxHealth => _MaxHealth;
        
        // Move
        /// <summary>
        /// Move Speed of the Character.
        /// </summary>
        public float moveSpeed => _MoveSpeed;

        // Attack
        /// <summary>
        /// Attack of the Character.
        /// </summary>
        public Attack attack => _Attack;
        
        // Icon
        /// <summary>
        /// Icon of the Character
        /// </summary>
        public Sprite icon => _Icon;
    }
}