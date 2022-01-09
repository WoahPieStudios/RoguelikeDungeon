using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Game.Characters;

public class UIEffectList : MonoBehaviour
{
    [SerializeField]
    Text _Text;
    [SerializeField]
    Hero _Hero;

    // Update is called once per frame
    void Update()
    {
        _Text.text = Time.deltaTime.ToString() + " " + _Hero.effects.Length + "\n";
        foreach(Effect effect in _Hero.effects)
            _Text.text += effect.name + " " + effect.instanceId + "\n";
    }
}
