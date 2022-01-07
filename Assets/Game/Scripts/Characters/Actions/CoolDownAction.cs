using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class CoolDownAction : MonoBehaviour, IAction<CharacterBase>, ICoolDown
    {
        [SerializeField]
        float _CoolDownTime;

        CharacterBase _Target;

        bool _IsActive = false;

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

        public bool isActive => _IsActive;

        public CharacterBase target => _Target;

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

                yield return new WaitForEndOfFrame();
            }

            _CurrentCoolDownTime = 0;

            _IsCoolingDown = false;
        }

        protected void Begin(CharacterBase target)
        {
            _IsActive = true;

            _Target = target;
        }

        /// <summary>
        /// Ends Action. Cool Down Starts afterwards.
        /// </summary>
        public virtual void End()
        {
            _IsActive = false;

            _CoolDownCoroutine = StartCoroutine(CoolDown());
        }

        public virtual void StopCoolDown()
        {
            if(_CoolDownCoroutine != null)
            {
                _IsCoolingDown = false;

                StopCoroutine(_CoolDownCoroutine);
            }
        }

        public virtual void ForceStart()
        {
            _IsActive = true;
        }
    }
}