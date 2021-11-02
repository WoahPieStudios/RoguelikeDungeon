using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;

[CreateAssetMenu(menuName = "Data/TestAttack")]
public class TestAttack : Attack
{
    [SerializeField]
    LayerMask _CharacterLayer;
    protected override IEnumerator Tick()
    {
        Debug.Log(target.FaceNearestCharacter(range, _CharacterLayer));
        
        yield return null;

        End();
    }
}
