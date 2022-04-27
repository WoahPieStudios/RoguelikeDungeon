using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Properties;

namespace Game.Characters.Properties
{
    public interface IValueRange : IPropertyHandler
    {
        public event Action<float> onCurrentValueChangeEvent;

        public float maxValue { get; }
        public float currentValue { get; set; }

        public void SetCurrentValueWithoutEvent(float value);
        
        /// <summary>
        /// Adds to the Health of the Character
        /// </summary>
        /// <param name="health">Amount to be added</param>
        public void AddValue(float value);

        /// <summary>
        /// Reduces the Health of the Character
        /// </summary>
        /// <param name="damage">Amount to be reduced the health by</param>
        public void ReduceValue(float value);

        public void Reset();
    }
}