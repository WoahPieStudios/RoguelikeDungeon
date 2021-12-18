using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    [RootCreatableAsset]
    public abstract class Action : ScriptableObject, IIcon, ICopyable
    {
        [SerializeField]
        Sprite _Icon;

        bool _IsActive = false;

        bool _IsCopy;

        int _InstanceId;

        Coroutine _TickCoroutine;

        CharacterBase _Target;

        /// <summary>
        /// Target of the Action.
        /// </summary>
        protected CharacterBase target => _Target;

        /// <summary>
        /// To see if Action is Active.
        /// </summary>
        public bool isActive => _IsActive;

        /// <summary>
        /// Icon of the Action.
        /// </summary>
        public Sprite icon => _Icon;

        /// <summary>
        /// To determine if Action is a copy or not. **DO NOT CHANGE THE VALUE**
        /// </summary>
        /// <value></value>
        public bool isCopied => _IsCopy;

        /// <summary>
        /// Unique ID of the Action. **DO NOT CHANGE THE VALUE**
        /// </summary>
        /// <returns></returns>
        public int instanceId => _InstanceId;

        // Tick, to update your action, also in IEnumerator for to set update in Fixed or normal Update. **DO NOT SETACTIVE(FALSE) GAMEOBJECT AS IT WILL STOP ALL TICKS**
        /// <summary>
        /// Updates the action. **DO NOT SETACTIVE(FALSE) GAMEOBJECT AS IT WILL STOP ALL TICKS**
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerator Tick();

        /// <summary>
        /// Starts the Action, Sets up Variables and runs Tick
        /// </summary>
        /// <param name="target"></param>
        protected void Begin(CharacterBase target)
        {
            _IsActive = true;

            _Target = target;

            _TickCoroutine = target.StartCoroutine(Tick());
        }

        /// <summary>
        /// Ends Action and stops Tick if its still running. Can be called by itself or outside.
        /// </summary>
        public virtual void End()
        {
            _IsActive = false;

            if(_TickCoroutine != null)
                _Target.StopCoroutine(_TickCoroutine);
        }

        /// <summary>
        /// Creates copy. **DO NOT TOUCH**
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T CreateCopy<T>() where T : Action
        {
            T copy = Instantiate(this) as T;

            copy._InstanceId = GetInstanceID();
            copy._IsCopy = true;

            return copy;
        }
    }
}