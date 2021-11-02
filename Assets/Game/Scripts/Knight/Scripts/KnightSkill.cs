using System.Collections;
using Game.Characters;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Knight/Skill")]
public class KnightSkill : Skill
{
    protected override IEnumerator Tick()
    {
        yield return null;
        End();
    }

    protected override void OnCooldown()
    {
        
    }
}
