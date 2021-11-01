using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public static class MethodExtensions
    {
        public static Enemy FaceNearestEnemy(this CharacterBase characterBase, float radius, LayerMask enemyLayer)
        {
            Enemy enemy = Utilities.GetNearestEnemy(characterBase.transform.position, radius, enemyLayer);

            if(enemy)
            {
                Vector2 direction = enemy.transform.position - characterBase.transform.position;

                characterBase.Orient(Vector2Int.RoundToInt(direction.normalized));
            }

            return enemy;
        }
    }
}