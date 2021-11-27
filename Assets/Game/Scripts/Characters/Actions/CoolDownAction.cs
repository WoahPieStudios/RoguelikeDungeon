using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class CoolDownAction : Action, ICoolDown
    {
        [SerializeField]
        readonly float _CoolDownTime;

        float _CurrentCoolDownTime;

        bool _IsCoolingDown = false;
        
        Coroutine _CoolDownCoroutine;
        
        /// <summary>
        /// Cool Down Time after ending the Action.
        /// </summary>
        public float coolDownTime => _CoolDownTime;

        /// <summary>
        /// Current Time of the cooldown;
        /// </summary>
        public float currentCoolDownTime => _CurrentCoolDownTime;

        /// <summary>
        /// Determines if the Actino is Coolin Down.
        /// </summary>
        public bool isCoolingDown => _IsCoolingDown;


        /// <summary>
        /// Is called whenever the Action is cooling down. **Could be removed one day if no one uses it**
        /// </summary>
        protected abstract void OnCooldown();

        /// <summary>
        /// Cool Down Sequence.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator CoolDown()
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

        /// <summary>
        /// Ends Action. Cool Down Starts afterwards.
        /// </summary>
        public override void End()
        {
            base.End();

            _CoolDownCoroutine = target.StartCoroutine(CoolDown());
        }

        public virtual void StopCoolDown()
        {
            if(_CoolDownCoroutine != null)
            {
                _IsCoolingDown = false;

                target.StopCoroutine(_CoolDownCoroutine);
            }
        }
    }
}