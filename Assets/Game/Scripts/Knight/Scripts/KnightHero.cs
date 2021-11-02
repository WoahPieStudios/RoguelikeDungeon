using Game.Characters;
using UnityEngine;

public class KnightHero : Hero
{
    [SerializeField] private HeroData data;
    private Animator _animator;
    private bool _isFlipped;
    
    protected override void Awake()
    {
        base.Awake();
        AssignData(data);
        _animator = GetComponent<Animator>();
    }

    public override bool UseAttack()
    {
        print("Knight Attacking");
        _animator.SetTrigger("Attack1");
        return base.UseAttack();
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
