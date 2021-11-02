using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public static class Utilities
    {
        public static T GetNearestCharacter<T>(Vector3 center, float radius, LayerMask characterLayer) where T : CharacterBase
        {
            IEnumerable<(T, float)> characterHits = GetCharacters(center, radius, characterLayer).
                Select(hit =>(hit.collider.GetComponent<T>(), hit.distance));

            T nearestCharacter = null;

            if(characterHits.Any())
            {
                float minDistance = characterHits.Min(enemyHit => enemyHit.Item2);

                nearestCharacter = characterHits.First(enemyHit => enemyHit.Item2 == minDistance).Item1;
            }

            return nearestCharacter;
        }

        public static RaycastHit2D[] GetCharacters(Vector3 center, float radius, LayerMask characterLayer)
        {
            return Physics2D.CircleCastAll(center, radius, Vector2.zero, 0, characterLayer).Where(hit => hit.collider.TryGetComponent<CharacterBase>(out CharacterBase nearest)).ToArray();
        }

        public static T[] GetCharacters<T>(Vector3 center, float radius, LayerMask characterLayer) where T : CharacterBase
        {
            IEnumerable<T> characterHits = GetCharacters(center, radius, characterLayer).
                Select(hit => hit.collider.GetComponent<T>());

            return characterHits.ToArray();
        }
    }
}