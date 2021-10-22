using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    [CreateAssetMenu(menuName = "Data/HeroData")]
    public class HeroData : CharacterData<Hero>
    {
        [SerializeField]
        int _MaxMana;
        [SerializeField]
        Passive<Hero>[] _Passives;
        [SerializeField]
        Skill _Skill;
        [SerializeField]
        Ultimate _Ultimate;

        public int maxMana => _MaxMana;

        public Passive<Hero>[] passives => _Passives;

        public Skill skill => _Skill;

        public Ultimate ultimate => _Ultimate;
    }
}