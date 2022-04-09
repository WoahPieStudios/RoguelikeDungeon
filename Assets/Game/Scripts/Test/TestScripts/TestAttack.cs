using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Actions;

namespace Game.Characters.Test
{
    public class TestAttack : Attack<Character>
    {
        Coroutine _TickCoroutine;

        public override float damage => throw new System.NotImplementedException();

        public override float range => throw new System.NotImplementedException();

        public override float speed => throw new System.NotImplementedException();

        public override float coolDownTime => throw new System.NotImplementedException();

        IEnumerator Tick()
        {
            owner.FaceNearestCharacter(range);
            // target.FaceNearestCharacter<CharacterBase>(range, _CharacterLayer);

            // Utilities.GetCharacters(Vector3.zero, 5, _CharacterLayer);
            // Utilities.GetCharacters<CharacterBase>(Vector3.zero, 5, _CharacterLayer);
            // Utilities.GetNearestCharacter<CharacterBase>(Vector3.zero, 5, _CharacterLayer);
            
            yield return new WaitForSeconds(5);

            End();
        }

        protected override void OnUse()
        {
            base.OnUse();

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