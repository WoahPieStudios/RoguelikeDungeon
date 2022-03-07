using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Animations;
using Game.Characters.Actions;

public class SkeletonAttack : Attack
{
    [SerializeField]
    private AnimationData _AttackData;

    private AnimationHandler _AnimationHandler;

    protected override void Awake()
    {
        base.Awake();

        _AnimationHandler = GetComponent<AnimationHandler>();
    }

    private void Start() 
    {
        _AnimationHandler.AddAnimationData(_AttackData, Test);
    }

    protected override void Begin()
    {
        base.Begin();

        _AnimationHandler.CrossFadePlay(_AttackData, 0.1f);
    }

    private void Test()
    {
        Debug.LogError("Blah");

    }

    public override void End()
    {
        base.End();


        // _AnimationHandler.Stop(_AttackData);
    }
}
