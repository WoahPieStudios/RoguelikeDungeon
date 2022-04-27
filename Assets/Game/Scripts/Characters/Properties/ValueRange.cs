using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Properties;

namespace Game.Characters.Properties
{
    public abstract class ValueRange : IValueRange
    {
        [SerializeField]
        Property _MaxValue = new Property(MaxValueProperty);
        [SerializeField]
        float _CurrentValue = 0;

        List<IProperty> _PropertyList = new List<IProperty>();
        
        public IProperty[] properties => _PropertyList.ToArray();

        public float maxValue => _MaxValue;

        public float currentValue { get => _CurrentValue; set => _CurrentValue = value; }


        public event Action<float> onCurrentValueChangeEvent;

        public const string MaxValueProperty = "maxValue";

        public ValueRange()
        {
            _PropertyList.Add(_MaxValue);
        }

        protected virtual void SetCurrentValue(float newValue)
        {
            SetCurrentValueWithoutEvent(newValue);

            onCurrentValueChangeEvent(currentValue);
        }

        public void AddValue(float value)
        {
            SetCurrentValue(currentValue + value);
        }

        public void ReduceValue(float value)
        {
            SetCurrentValue(currentValue - value);
        }

        public void SetCurrentValueWithoutEvent(float value)
        {
            if(value < 0)
                value = 0;

            else if(value > maxValue)
                value = maxValue;

            _CurrentValue = value;
        }

        public void Reset()
        {
            SetCurrentValue(maxValue);
        }

        public bool ContainsProperty(string property)
        {
            return _PropertyList.Any(p => p.name == property);
        }

        public IProperty GetProperty(string property)
        {
            return _PropertyList.FirstOrDefault(p => p.name == property);
        }
    }
}