using System.Collections;
using Game.Characters;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Knight/Attack")]
public class KnightAttack : Attack
{


    
    protected override IEnumerator Tick()
    {

        
        yield return null;
        End();
    }

    
}
