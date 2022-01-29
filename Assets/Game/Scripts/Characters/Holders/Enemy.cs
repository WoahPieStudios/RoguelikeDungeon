using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public class Enemy : CharacterBase//Character<EnemyData>
    {
        [SerializeField] private int health;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private bool _takingDamage;
        protected override void Awake()
        {
            base.Awake();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public virtual void TakeDamage(int damage)
        {
            if (_takingDamage) return;
            StartCoroutine(DamageRoutine());
            health -= damage;
        }

        IEnumerator DamageRoutine()
        {
            _takingDamage = true;
            _spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(1.0f);
            _spriteRenderer.color = Color.white;
            _takingDamage = false;
            yield return new WaitForSeconds(0.3f);
            if(health <= 0)
                Destroy(gameObject);
        }
        
    }
}