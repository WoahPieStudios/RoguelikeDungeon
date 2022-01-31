using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public static class Utilities
    {
        /// <summary>
        /// Gets Characters inside the circle. 
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="exceptCharacters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Tuple<T, float>[] GetCharactersAndDistances<T>(Vector3 center, float radius, params Character[] exceptCharacters) where T : Character
        {
            return Character.characters.Except(exceptCharacters).
                Select(character => new Tuple<Character, float>(character, (center - character.transform.position).magnitude)).
                Where(item => item.Item1 is T && item.Item2 <= radius).
                Select(item => new Tuple<T, float>(item.Item1 as T, item.Item2)).
                ToArray();
        }

        /// <summary>
        /// Gets Characters inside the circle. 
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="exceptCharacters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>w
        public static T[] GetCharacters<T>(Vector3 center, float radius, params Character[] exceptCharacters) where T : Character
        {
            return GetCharactersAndDistances<T>(center, radius, exceptCharacters).
                Select(t => t.Item1).
                ToArray();
        }

        /// <summary>
        /// Gets nearest Character inside the circle.
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="exceptCharacters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetNearestCharacter<T>(Vector3 center, float radius, params Character[] exceptCharacters) where T : Character
        {
            return GetCharactersAndDistances<T>(center, radius, exceptCharacters)?.OrderBy(item => item.Item2)?.
                FirstOrDefault()?.Item1;
        }
    }
}