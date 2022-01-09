using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CreatableAssetAttribute : Attribute
    {
        string[] _Categories;

        public string[] categories => _Categories;

        public CreatableAssetAttribute()
        {
            _Categories = new string[] { "Uncategorized" };
        }

        public CreatableAssetAttribute(params string[] categories)
        {
            _Categories = categories;
        }
    }
}