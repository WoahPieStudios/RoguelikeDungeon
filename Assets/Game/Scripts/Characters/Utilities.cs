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
        public static Tuple<T, float>[] GetCharacters<T>(Vector3 center, float radius, params CharacterBase[] exceptCharacters) where T : CharacterBase
        {
            return CharacterBase.characters.Except(exceptCharacters).
                Select(character => new Tuple<CharacterBase, float>(character, (center - character.transform.position).magnitude)).
                Where(item => item.Item1 is T && item.Item2 <= radius).
                Select(item => new Tuple<T, float>(item.Item1 as T, item.Item2)).
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
        public static T GetNearestCharacter<T>(Vector3 center, float radius, params CharacterBase[] exceptCharacters) where T : CharacterBase
        {
            return GetCharacters<T>(center, radius, exceptCharacters)?.OrderBy(item => item.Item2)?.
                FirstOrDefault()?.Item1;
        }
    }
}