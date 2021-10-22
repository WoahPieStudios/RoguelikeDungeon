using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public class Hero : Character<Hero>
    {
        int _CurrentMana;

        public int currentMana => _CurrentMana;
        public int maxMana => data.maxMana;

        public void AddMana(int mana)
        {
            int newMana = _CurrentMana + mana;

            _CurrentMana = newMana > maxMana ? maxMana : newMana;
        }

        public void UseMana(int mana)
        {
            int newMana = _CurrentMana - mana;

            _CurrentMana = newMana > 0 ? newMana : 0;
        }
    }
}