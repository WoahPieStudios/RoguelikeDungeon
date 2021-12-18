using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;

[CreatableAsset]
public class TestAttack : Attack
{
    [SerializeField]
    LayerMask _CharacterLayer;
    protected override IEnumerator Tick()
    {
        target.FaceNearestCharacter(range, _CharacterLayer);
        // target.FaceNearestCharacter<CharacterBase>(range, _CharacterLayer);

        // Utilities.GetCharacters(Vector3.zero, 5, _CharacterLayer);
        // Utilities.GetCharacters<CharacterBase>(Vector3.zero, 5, _CharacterLayer);
        // Utilities.GetNearestCharacter<CharacterBase>(Vector3.zero, 5, _CharacterLayer);
        
        yield return null;

        End();
    }
}
