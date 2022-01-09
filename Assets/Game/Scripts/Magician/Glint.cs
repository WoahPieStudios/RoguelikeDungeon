using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.Magician
{
    [CreatableAsset("Magician")]
    public class Glint : PassiveEffect
    {
        [SerializeField]
        int _BonusDamage;

        public override void Stack(params Effect[] effects)
        {
            throw new System.NotImplementedException();
        }

        public override void StartEffect(CharacterBase sender, CharacterBase effected)
        {
            base.StartEffect(sender, effected);

            if (sender.attack is IBonusDamage)
                (sender.attack as IBonusDamage).bonusDamge = _BonusDamage;
        }

        public override void End()
        {
            base.End();

            if (sender.attack is IBonusDamage)
                (sender.attack as IBonusDamage).bonusDamge = 0;
        }
    }
}