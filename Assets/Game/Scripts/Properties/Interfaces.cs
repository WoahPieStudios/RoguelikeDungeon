using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Properties
{
    public interface IPropertyHandler
    {
        IProperty[] properties { get; }
        bool ContainsProperty(string property);
        IProperty GetProperty(string property);
    }

    public interface IProperty
    {
        string name { get; }
        float baseValue { get; set; }
        float valueAdded { get; set; }
    }
}