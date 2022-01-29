using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;

public class TestHero : Hero
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
            Debug.Log("Ultimate " + UseUltimate());

        if(Input.GetKeyDown(KeyCode.D))
            Debug.Log("Skill " + UseSkill());
        
        if(Input.GetKeyDown(KeyCode.S))
            Debug.Log("Attack " + UseAttack());
    }
}