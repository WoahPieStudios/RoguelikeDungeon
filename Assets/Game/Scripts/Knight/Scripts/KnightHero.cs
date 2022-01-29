using System;
using Game.Characters;
using UnityEngine;

public class KnightHero : Hero
{
    [SerializeField] private HeroData data;
    private Animator _animator;
    private bool _isFlipped;
    
        [Header("Attack Info")]
        [SerializeField] private float startTimeBtwAttack;
        [SerializeField] private Transform attackPos;
        [SerializeField] private float attackRange;
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private int damage;
        
        private float _timeBtwAttack;
        private bool _canAttack;
    
    protected override void Awake()
    {
        base.Awake();
        AssignData(data);
        _animator = GetComponent<Animator>();
    }
    
    private void Attack()
    {
        Debug.LogWarning(name + "Attacking");
        
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);
        foreach (var enemy in enemiesToDamage)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
        _timeBtwAttack = startTimeBtwAttack;
        
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position,attackRange);
    }

    public override bool UseAttack()
    {
        print("Knight Attacking");
        _animator.SetTrigger("Attack1");
        Attack();
        return base.UseAttack();
    }

    public override bool UseSkill()
    {
        _animator.SetTrigger("Attack2");
        return base.UseSkill();
    }

    public override bool UseUltimate()
    {
        _animator.SetTrigger("Attack3");
        return base.UseUltimate();
    }

    public override bool Move(Vector2 direction)
    {
        _animator.SetInteger("AnimState", direction != Vector2.zero ? 1 : 0);
        OrientPlayer(direction);
        return base.Move(direction);
    }

    protected virtual void OrientPlayer(Vector2 direction)
    {
        if (direction.x < 0)
        {
            _isFlipped = true;
        }
        else if (direction.x > 0)
        {
            _isFlipped = false;
        }
            
        transform.rotation = _isFlipped ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
    }
}
