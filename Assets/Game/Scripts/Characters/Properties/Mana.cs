using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters.Properties
{
    [Serializable]
    public class Mana
    {
        [SerializeField]
        float _MaxMana = 0;
        [SerializeField]
        float _CurrentMana = 0;

        public event Action<Mana, float> onUseManaEvent;
        public event Action<Mana, float> onAddManaEvent;
        public event Action<Mana> onDrainManaEvent;
        public event Action<Mana> onResetManaEvent;
        public event Action<Mana> onNewMaxManaEvent;

        public float maxMana => _MaxMana;

        public float currentMana => _CurrentMana;

        public void SetMaxManaWithoutEvent(float newMaxMana)
        {
            _MaxMana = newMaxMana > 0 ? newMaxMana : 0;
        }

        public void SetMaxMana(float newMaxMana)
        {
            SetMaxManaWithoutEvent(newMaxMana);

            onNewMaxManaEvent?.Invoke(this);
        }
        

        public void SetManaWithoutNotify(float newMana)
        {
            if(newMana < 0)
                _CurrentMana = 0;

            else if(newMana > maxMana)
                _CurrentMana = maxMana;
            
            else
                _CurrentMana = newMana;
        }

        public void AddMana(float mana)
        {
            SetManaWithoutNotify(_CurrentMana + mana);

            onAddManaEvent?.Invoke(this, mana);
        }

        public void UseMana(float mana)
        {
            SetManaWithoutNotify(_CurrentMana - mana);

            onUseManaEvent?.Invoke(this, mana);
        }

        public void ResetMana()
        {
            _CurrentMana = _MaxMana;

            onResetManaEvent?.Invoke(this);
        }

        public void DrainMana()
        {
            _CurrentMana = 0;

            onDrainManaEvent?.Invoke(this);
        }
    }
}