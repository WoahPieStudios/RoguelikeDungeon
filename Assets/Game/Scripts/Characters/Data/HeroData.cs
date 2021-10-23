using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
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

        public int maxMana => _MaxMana;

        public PassiveEffect[] passiveEffects => _PassiveEffects;

        public Skill skill => _Skill;

        public Ultimate ultimate => _Ultimate;
    }
}