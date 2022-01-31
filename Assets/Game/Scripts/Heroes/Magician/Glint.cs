using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Heroes;
using Game.Characters;
using Game.Characters.Effects;

namespace Game.Heroes.Magician
{
    public class Glint : PassiveEffect
    {
        [SerializeField]
        int _BonusDamage;

        public override void StartEffect(IEffectable sender, IEffectable receiver)
        {
            base.StartEffect(sender, receiver);

            Hero hero = receiver as Hero;

            if (hero.attack is IBonusDamage)
                (hero.attack as IBonusDamage).bonusDamge = _BonusDamage;
        }

        public override void End()
        {
            base.End();

            Hero hero = receiver as Hero;

            if (hero.attack is IBonusDamage)
                (hero.attack as IBonusDamage).bonusDamge = 0;
        }

        public override bool CanUse(Hero hero)
        {
            return true;
        }
    }
}