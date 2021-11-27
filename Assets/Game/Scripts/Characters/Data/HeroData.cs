using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public class HeroData : CharacterData
    {
        [SerializeField]
        readonly int _MaxMana;
        [SerializeField]
        readonly PassiveEffect[] _PassiveEffects;
        [SerializeField]
        readonly Skill _Skill;
        [SerializeField]
        readonly Ultimate _Ultimate;

        /// <summary>
        /// Max Mana of the Hero
        /// </summary>
        public int maxMana => _MaxMana;

        /// <summary>
        /// Passive Effects of the Hero
        /// </summary>
        public PassiveEffect[] passiveEffects => _PassiveEffects;

        /// <summary>
        /// Skill of the Hero
        /// </summary>
        public Skill skill => _Skill;

        /// <summary>
        /// Ultimate of the Hero.
        /// </summary>
        public Ultimate ultimate => _Ultimate;
    }
}