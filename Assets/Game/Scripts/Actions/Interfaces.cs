using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Properties;

namespace Game.Actions
{
    public interface IAction : IPropertyHandler
    {
        void Use();
        void End();
    }
}