using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RootCreatableAssetAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Class)]
    public class CreatableAssetAttribute : Attribute
    {

    }
}