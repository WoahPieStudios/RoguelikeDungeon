using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;

public class TestHero : Hero
{
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if(input != Vector2.zero)
            orientation.FaceDirection(Vector2Int.RoundToInt(input));

        movement.Move(input);


        
        if(Input.GetKeyDown(KeyCode.U))
            Debug.Log("Attack " + attack.Use(this));

        if(Input.GetKeyDown(KeyCode.I))
            Debug.Log("Skill " + skill.Use(this));

        if(Input.GetKeyDown(KeyCode.O))
            Debug.Log("Ultimate " + ultimate.Use(this));
    }
}