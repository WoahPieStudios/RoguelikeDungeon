using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.Magician
{
    [CreatableAsset("Magician")]
    public class MagicianAttack : Attack, IBonusDamage
    {
        [SerializeField]
        Sprite _LaserSprite;
        [SerializeField]
        LayerMask _EnemyLayer;

        SpriteRenderer _LaserRenderer;

        Enemy _ClosestEnemy;
        
        public int bonusDamge { get; set; }

        protected override IEnumerator Tick()
        {
            if(!_ClosestEnemy)
                yield break;

            yield return null;

            Vector2 enemyDirection;


            enemyDirection = _ClosestEnemy.transform.position - target.transform.position;

            _LaserRenderer.gameObject.transform.position = target.transform.position + (Vector3)enemyDirection.normalized * (range / 2);
            _LaserRenderer.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, enemyDirection) * Quaternion.Euler(0, 0, 90);
            _LaserRenderer.size = new Vector2(range, _LaserRenderer.size.y);

            yield return new WaitForEndOfFrame();
            

            End();
        }

        public override bool CanUse(CharacterBase attacker)
        {
            _ClosestEnemy = Utilities.GetCharacters(attacker.transform.position, range, _EnemyLayer)?.Select(r => r.collider.GetComponent<Enemy>()).Where(e => e).First();

            return base.CanUse(attacker) && _ClosestEnemy;
        }

        public override void Use(CharacterBase attacker)
        {
            base.Use(attacker);

            if(!_LaserRenderer)
            {
                _LaserRenderer = new GameObject("Laser").AddComponent<SpriteRenderer>();
                _LaserRenderer.sprite = _LaserSprite;
                _LaserRenderer.drawMode = SpriteDrawMode.Sliced;
            }
        }
    }
}