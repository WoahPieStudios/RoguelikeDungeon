using Game.Characters;
using UnityEngine;

public class KnightHero : Hero
{
    [SerializeField] private HeroData data;

    protected override void Awake()
    {
        base.Awake();
        AssignData(data);
    }

    public override bool UseAttack()
    {
        print("Knight Attacking");
        return base.UseAttack();
    }

    public override bool Move(Vector2 direction)
    {
        return base.Move(direction);
    }
}
