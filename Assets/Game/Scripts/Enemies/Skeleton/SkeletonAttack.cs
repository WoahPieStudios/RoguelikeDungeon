using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Animations;
using Game.Characters.Actions;

public class SkeletonAttack : Attack
{
    [SerializeField]
    private AnimationClip _AttackClip;

    private AnimationHandler _AnimationHandler;

    private const string _AttackAnimName = "Attack";

    protected override void Awake()
    {
        base.Awake();

        _AnimationHandler = GetComponent<AnimationHandler>();
    }

    private void Start() 
    {
        _AnimationHandler.AddAnimation(_AttackAnimName, _AttackClip, 1, End);
    }

    protected override void Begin()
    {
        base.Begin();

        _AnimationHandler.CrossFadePlay(_AttackAnimName, 0.1f);
    }

    public override void End()
    {
        base.End();

        _AnimationHandler.Stop(_AttackAnimName);
    }
}
