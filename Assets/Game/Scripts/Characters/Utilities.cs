using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public static class Utilities
    {
        public static Enemy GetNearestEnemy(Vector3 center, float radius, LayerMask enemyLayer)
        {
            IEnumerable<Tuple<Enemy, float>> enemyHits = Physics2D.CircleCastAll(center, radius, Vector2.zero).
                Where(hit => hit.collider.TryGetComponent<Enemy>(out Enemy enemy)).
                Select(hit => new Tuple<Enemy, float>(hit.collider.GetComponent<Enemy>(), hit.distance));

            Enemy enemy = null;

            if(enemyHits.Any())
            {
                float minDistance = enemyHits.Min(enemyHit => enemyHit.Item2);

                enemy = enemyHits.First(enemyHit => enemyHit.Item2 == minDistance).Item1;
            }

            return enemy;
        }
    }
}