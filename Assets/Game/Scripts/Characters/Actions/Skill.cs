using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class Skill : Action<Skill>
    {
        [SerializeField]
        float _CoolDownTime;

        float _CurrentTime;

        bool _IsCoolingDown = false;

        Hero _Hero;

        protected Hero hero => _Hero;        

        protected event System.Action onCoolDownEnd;

        public float coolDownTime => _CoolDownTime;
        public float currentTime => _CurrentTime;

        public bool isCoolingDown => _IsCoolingDown;

        protected virtual void StartCoolDown()
        {
            _IsCoolingDown = true;

            _CurrentTime = _CoolDownTime;
        }

        public virtual void Use(Hero hero)
        {
            _Hero = hero;

            Begin();
        }

        public virtual bool CanUse()
        {
            return !isActive && !_IsCoolingDown;
        }

        public override void Tick()
        {
            if(_IsCoolingDown)
            {
                _CurrentTime -= Time.deltaTime;

                if(_CurrentTime <= 0)
                {
                    _IsCoolingDown = false;

                    _CurrentTime = 0;

                    onCoolDownEnd?.Invoke();
                }
            }
        }
    }
}