using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Interfaces;

namespace Game.Characters
{
    [Serializable]
    public class Mana : IMana
    {
        [SerializeField]
        int _MaxMana = 0;
        [SerializeField]
        int _CurrentMana = 0;

        public event Action<IMana, int> onUseManaEvent;
        public event Action<IMana, int> onAddManaEvent;
        public event System.Action onDrainManaEvent;
        public event System.Action onResetManaEvent;

        public int maxMana => _MaxMana;

        public int currentMana => _CurrentMana;

        public Mana() 
        {
            ResetMana();
        }

        public void AddMana(int mana)
        {
            int newMana = _CurrentMana + mana;

            _CurrentMana = newMana > maxMana ? maxMana : newMana;

            onAddManaEvent?.Invoke(this, mana);
        }

        public void UseMana(int mana)
        {
            int newMana = _CurrentMana - mana;

            _CurrentMana = newMana > 0 ? newMana : 0;

            onUseManaEvent?.Invoke(this, mana);
        }

        public void ResetMana()
        {
            _CurrentMana = _MaxMana;

            onResetManaEvent?.Invoke();
        }

        public void DrainMana()
        {
            _CurrentMana = 0;

            onDrainManaEvent?.Invoke();
        }
    }
}