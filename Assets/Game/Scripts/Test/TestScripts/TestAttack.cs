using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Actions;

namespace Game.Characters.Test
{
    public class TestAttack : Attack
    {
        Coroutine _TickCoroutine;

        IEnumerator Tick()
        {
            (owner as Character).FaceNearestCharacter(range);
            // target.FaceNearestCharacter<CharacterBase>(range, _CharacterLayer);

            // Utilities.GetCharacters(Vector3.zero, 5, _CharacterLayer);
            // Utilities.GetCharacters<CharacterBase>(Vector3.zero, 5, _CharacterLayer);
            // Utilities.GetNearestCharacter<CharacterBase>(Vector3.zero, 5, _CharacterLayer);
            
            yield return new WaitForSeconds(5);

            End();
        }

        protected override void Begin()
        {
            base.Begin();

            _TickCoroutine = StartCoroutine(Tick());
        }

        public override void End()
        {
            base.End();

            if(_TickCoroutine != null)
                StopCoroutine(_TickCoroutine);
        }
    }
}