using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;

public class TestAttack : Attack
{
    Coroutine _TickCoroutine;

    IEnumerator Tick()
    {
        target.FaceNearestCharacter(range);
        // target.FaceNearestCharacter<CharacterBase>(range, _CharacterLayer);

        // Utilities.GetCharacters(Vector3.zero, 5, _CharacterLayer);
        // Utilities.GetCharacters<CharacterBase>(Vector3.zero, 5, _CharacterLayer);
        // Utilities.GetNearestCharacter<CharacterBase>(Vector3.zero, 5, _CharacterLayer);
        
        yield return null;

        End();
    }

    public override bool Use(CharacterBase attacker)
    {
        bool canUse = base.Use(attacker);

        _TickCoroutine = target.StartCoroutine(Tick());

        return canUse;
    }

    public override void End()
    {
        base.End();

        if(_TickCoroutine != null)
            target.StopCoroutine(_TickCoroutine);
    }
}
