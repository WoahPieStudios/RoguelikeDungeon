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

        Coroutine _TickCoroutine;

        IEnumerator Tick()
        {
            yield return null;

            End();
        }

        int OnUseBonusDamge()
        {
            _TickCoroutine = StartCoroutine(Tick());

            return _BonusDamage;
        }

        public override void StartEffect(IEffectable sender, IEffectable receiver)
        {
            base.StartEffect(sender, receiver);

            Hero hero = receiver as Hero;

            if (hero.attack is IGlintBonusDamage)
                (hero.attack as IGlintBonusDamage).onUseBonusDamageEvent += OnUseBonusDamge;
        }

        public override void End()
        {
            base.End();

            Hero hero = receiver as Hero;

            if (hero.attack is IGlintBonusDamage)
                (hero.attack as IGlintBonusDamage).onUseBonusDamageEvent -= OnUseBonusDamge;

            if(_TickCoroutine != null)
                StopCoroutine(_TickCoroutine);
        }
    }
}