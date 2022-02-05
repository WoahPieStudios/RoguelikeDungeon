using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Heroes
{
    public interface IGlintBonusDamage
    {
        event System.Func<int> onUseBonusDamageEvent;
    }
}