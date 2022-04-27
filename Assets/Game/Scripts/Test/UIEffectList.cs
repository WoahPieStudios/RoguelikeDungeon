using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Game.Characters;
using Game.Characters.Effects;
using Game.Effects;

public class UIEffectList : MonoBehaviour
{
    [SerializeField]
    Text _Text;
    [SerializeField]
    ActiveCharacter _Character;

    // Update is called once per frame
    void Update()
    {
        if(!_Character && _Character.effects.Length <= 0)
            return;

        _Text.text = Time.deltaTime.ToString() + " " + _Character.effects?.Length + "\n";

        foreach(Effect effect in _Character.effects)
            _Text.text += effect.name + " " + effect.instanceId + "\n";
    }
}
