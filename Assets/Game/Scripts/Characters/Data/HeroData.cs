using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    [CreatableAsset]
    public class HeroData : CharacterData
    {
        [SerializeField]
        int _MaxMana;
        [SerializeField]
        PassiveEffect[] _PassiveEffects;
        [SerializeField]
        Skill _Skill;
        [SerializeField]
        Ultimate _Ultimate;

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