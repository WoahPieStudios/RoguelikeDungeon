using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;

public class Magician : Hero
{
    [SerializeField] HeroData heroData;

    private void Awake()
    {
        AssignData(heroData);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Debug.Log("Ultimate " + UseUltimate());

        if (Input.GetKeyDown(KeyCode.D))
            Debug.Log("Skill " + UseSkill());

        if (Input.GetKeyDown(KeyCode.S))
            Debug.Log("Attack " + UseAttack());
    }
}
