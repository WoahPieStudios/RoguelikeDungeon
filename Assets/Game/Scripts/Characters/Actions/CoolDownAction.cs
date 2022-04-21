using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Upgrades;
using Game.Properties;
using Game.Actions;

namespace Game.Characters.Actions
{
    public abstract class CoolDownAction<T> : CharacterAction<T>, ICoolDownAction where T : Character
    {
        [SerializeField]
        Property _CoolDownTime = new Property(CoolDownTimeProperty);

        float _CurrentCoolDownTime;

        bool _IsCoolingDown = false;
        
        Coroutine _CoolDownCoroutine;
        
        /// <summary>
        /// Cool Down Time after ending the Action.
        /// </summary>
        public Property coolDownTime => _CoolDownTime;
        /// <summary>
        /// Current Time of the cooldown;
        /// </summary>
        public float currentCoolDownTime => _CurrentCoolDownTime;

        /// <summary>
        /// Determines if the Actino is Coolin Down.
        /// </summary>
        public bool isCoolingDown => _IsCoolingDown;
        
        public const string CoolDownTimeProperty = "coolDownTime";

        protected override void Awake()
        {
            base.Awake();
            
            propertyList.Add(coolDownTime);
        }

        protected override bool CanUse()
        {
            return base.CanUse() && !isCoolingDown;
        }

        /// <summary>
        /// Cool Down Sequence.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator CoolDown()
        {
            _CurrentCoolDownTime  = coolDownTime;

            _IsCoolingDown = true;

            while(_CurrentCoolDownTime > 0)
            {
                _CurrentCoolDownTime -= Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }
            
            StopCoolDown();
        }

        /// <summary>
        /// Ends Action. Cool Down Starts afterwards.
        /// </summary>
        public override void End()
        {
            base.End();
            
            StartCoolDown();
        }

        public virtual void StartCoolDown()
        {
            _CoolDownCoroutine = StartCoroutine(CoolDown());
        }

        public virtual void StopCoolDown()
        {
            if(!_IsCoolingDown)
                return;
                
            _CurrentCoolDownTime = 0;

            _IsCoolingDown = false;
                
            if(_CoolDownCoroutine != null)
                StopCoroutine(_CoolDownCoroutine);
        }
    }
}