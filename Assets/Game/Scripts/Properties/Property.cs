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
        float _StartValue;

        string _Name;

        public string name => _Name;

        public float startValue { get => _StartValue; set => _StartValue = value; }
        public float valueAdded { get; set; } = 0;

        public Property(string name)
        {
            _Name = name;
        }

        public static implicit operator float (Property property)
        {
            return property.startValue + property.valueAdded;
        }
    }
}