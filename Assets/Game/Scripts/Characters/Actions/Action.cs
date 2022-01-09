using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class Action : ScriptableObject, IAction<CharacterBase>, IIcon, ICloneable
    {
        [SerializeField]
        Sprite _Icon;

        bool _IsActive = false;

        bool _IsCopy;

        int _InstanceId;

        CharacterBase _Target;

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

        /// <summary>
        /// Target of the Action.
        /// </summary>
        public CharacterBase target => _Target;

        /// <summary>
        /// Starts the Action, Sets up Variables and runs Tick
        /// </summary>
        /// <param name="target"></param>
        protected void Begin(CharacterBase target)
        {
            _IsActive = true;

            _Target = target;
        }

        /// <summary>
        /// Force Starts the action.
        /// </summary>
        public virtual void ForceStart()
        {
            _IsActive = true;
        }

        /// <summary>
        /// Ends Action and stops Tick if its still running. Can be called by itself or outside.
        /// </summary>
        public virtual void End()
        {
            _IsActive = false;
        }

        /// <summary>
        /// Creates copy. **DO NOT TOUCH**
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T CreateClone<T>() where T : Object, ICloneable
        {
            T copy = Instantiate(this) as T;

            copy.InitializeClone(GetInstanceID());

            return copy;
        }

        public void InitializeClone(int instanceid)
        {
            _InstanceId = instanceid;

            _IsCopy = true;
        }
    }
}