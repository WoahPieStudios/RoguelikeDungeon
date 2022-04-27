using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Properties
{
    [Serializable]
    public class Property : IProperty
    {
        [SerializeField]
        float _BaseValue;

        string _Name;

        public string name => _Name;

        public float baseValue { get => _BaseValue; set => _BaseValue = value; }
        public float valueAdded { get; set; } = 0;

        public Property(string name)
        {
            _Name = name;
        }

        public static implicit operator float (Property property)
        {
            return property.baseValue + property.valueAdded;
        }
    }
}