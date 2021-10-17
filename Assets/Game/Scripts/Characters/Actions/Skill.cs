using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class Skill : Action
    {
        [SerializeField]
        float _CoolDownTime;

        float _CurrentTime;

        public virtual void Use(Hero hero)
        {
            StartAction();

            _CoolDownTime = 0;
        }

        public override void Tick()
        {
            _CurrentTime += Time.deltaTime;
        }
    }
}