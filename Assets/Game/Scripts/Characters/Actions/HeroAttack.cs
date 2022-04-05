using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.Actions
{
    public abstract class HeroAttack : Attack<Hero>
    {
        [SerializeField]
        float _ManaGainOnHit;

        public float manaGainOnHit => _ManaGainOnHit;
    }
}