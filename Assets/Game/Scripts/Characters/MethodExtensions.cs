using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public static class MethodExtensions
    {
        /// <summary>
        /// Orients the Character towards the nearest Character.
        /// </summary>
        /// <param name="characterBase">Character to be ignored when finding the nearest character</param>
        /// <param name="radius">Radius around the character</param>
        /// <param name="characterLayer">Layer Mask for where the Characters are</param>
        /// <typeparam name="T">Generic For what class the Character is derived from. (Hero, Enemy, or CharacterBase)</typeparam>
        /// <returns>Returns the Nearest T Character</returns>
        public static T FaceNearestCharacter<T>(this CharacterBase characterBase, float radius) where T : CharacterBase
        {
            T nearestCharacter = Utilities.GetNearestCharacter<T>(characterBase.transform.position, radius, characterBase);

            if(nearestCharacter)
            {
                Vector2 direction = nearestCharacter.transform.position - characterBase.transform.position;

                characterBase.Orient(Vector2Int.RoundToInt(direction.normalized));
            }

            return nearestCharacter;
        }

        /// <summary>
        /// Orients the Character towards the nearest Character.
        /// </summary>
        /// <param name="characterBase">Character to be ignored when finding the nearest character</param>
        /// <param name="radius">Radius around the character</param>
        /// <param name="characterLayer">Layer Mask for where the Characters are</param>
        /// <returns>Returns the Nearest Character</returns>
        public static CharacterBase FaceNearestCharacter(this CharacterBase characterBase, float radius)
        {
            return FaceNearestCharacter<CharacterBase>(characterBase, radius);
        }
    }
}