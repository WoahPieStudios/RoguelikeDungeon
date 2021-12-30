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
    TestHero _TestHero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _Text.text = Time.deltaTime.ToString() + " " + _TestHero.effects.Length + "\n";
        foreach(Effect effect in _TestHero.effects)
            _Text.text += effect.name + " " + effect.instanceId + "\n";
    }
}
