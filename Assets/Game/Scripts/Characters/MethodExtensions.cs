using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public static class MethodExtensions
    {
        public static T FaceNearestCharacter<T>(this CharacterBase characterBase, float radius, LayerMask characterLayer) where T : CharacterBase
        {
            IEnumerable<(T, float)> characterHits = Utilities.GetCharacters(characterBase.transform.position, radius, characterLayer).
                Where(character => character != characterBase).
                Select(hit => (hit.collider.GetComponent<T>(), hit.distance));

            T nearestCharacter = characterHits.First(c => characterHits.Min(c => c.Item2) == c.Item2).Item1;

            if(nearestCharacter)
            {
                Vector2 direction = nearestCharacter.transform.position - characterBase.transform.position;

                characterBase.Orient(Vector2Int.RoundToInt(direction.normalized));
            }

            return nearestCharacter;
        }

        public static CharacterBase FaceNearestCharacter(this CharacterBase characterBase, float radius, LayerMask characterLayer)
        {
            IEnumerable<Tuple<CharacterBase, float>> characterHits = Utilities.GetCharacters(characterBase.transform.position, radius, characterLayer).
                Where(character => character.collider.GetComponent<CharacterBase>() != characterBase).
                Select(hit => new Tuple<CharacterBase, float>(hit.collider.GetComponent<CharacterBase>(), hit.distance));

            Tuple<CharacterBase, float> nearestCharacterByDistance = characterHits.First(c => characterHits.Min(characterMin => characterMin.Item2) == c.Item2);

            CharacterBase nearestCharacter = nearestCharacterByDistance != null ? nearestCharacterByDistance.Item1 : null;

            if(nearestCharacter)
            {
                Vector2 direction = nearestCharacter.transform.position - characterBase.transform.position;

                characterBase.Orient(Vector2Int.FloorToInt(direction.normalized));
            }

            return nearestCharacter;
        }
    }
}