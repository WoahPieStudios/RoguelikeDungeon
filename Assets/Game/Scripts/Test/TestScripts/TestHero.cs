using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;
using Game.Characters.Effects;

namespace Game.Characters.Test
{
    public class TestHero : Hero
    {
        [SerializeField]
        float _InputLerpTime;

        Vector2 _Input;

        void Update()
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            // if(input != Vector2.zero)
            //     orientation.Orientate(Vector2Int.RoundToInt(input));

            orientation.Orientate(Vector2Int.CeilToInt(input));

            _Input = Vector2.Lerp(_Input, input, _InputLerpTime);

            movement.Move(_Input);

            if(Input.GetKeyDown(KeyCode.U))
                Debug.Log("Attack " + attack.Use());

            if(Input.GetKeyDown(KeyCode.I))
                Debug.Log("Skill " + skill.Use());

            if(Input.GetKeyDown(KeyCode.O))
                Debug.Log("Ultimate " + ultimate.Use());
        }
    }
}