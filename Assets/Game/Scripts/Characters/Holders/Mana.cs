using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters
{
    public class Mana : MonoBehaviour
    {
        [SerializeField]
        int _MaxMana;

        int _CurrentMana;

        public int maxMana => _MaxMana;

        public int currentMana => _CurrentMana;

        void Awake() 
        {
            ResetMana();    
        }

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

        public void ResetMana()
        {
            _CurrentMana = _MaxMana;
        }

        public void DrainMana()
        {
            _CurrentMana = 0;
        }
    }
}