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
        Passive[] _Passives;
        [SerializeField]
        Skill _Skill;
        [SerializeField]
        Ultimate _Ultimate;

        public int maxMana => _MaxMana;

        public Passive[] passives => _Passives;

        public Skill skill => _Skill;

        public Ultimate ultimate => _Ultimate;
    }
}