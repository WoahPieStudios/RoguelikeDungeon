using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class CoolDownAction : Action
    {
        [SerializeField]
        float _CoolDownTime;

        float _CurrentCoolDownTime;

        bool _IsCoolingDown = false;

        Coroutine _CoolDownCoroutine;
        
        public float coolDownTime => _CoolDownTime;
        public float currentCoolDownTime => _CurrentCoolDownTime;

        public bool isCoolingDown => _IsCoolingDown;

        public abstract void OnCooldown();

        IEnumerator CoolDown()
        {
            _CurrentCoolDownTime  = _CoolDownTime;

            _IsCoolingDown = true;

            while(_CurrentCoolDownTime > 0)
            {
                _CurrentCoolDownTime -= Time.deltaTime;

                OnCooldown();

                yield return new WaitForEndOfFrame();
            }

            _CurrentCoolDownTime = 0;

            _IsCoolingDown = false;
        }

        public override void End()
        {
            base.End();

            _CoolDownCoroutine = characterBase.StartCoroutine(CoolDown());
        }
    }
}