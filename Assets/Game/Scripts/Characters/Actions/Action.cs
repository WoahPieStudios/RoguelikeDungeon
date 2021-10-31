using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class Action : ScriptableObject, IIcon, ICopyable
    {
        [SerializeField]
        Sprite _Icon;

        bool _IsActive = false;

        Coroutine _TickCoroutine;

        CharacterBase _Target;

        protected CharacterBase target => _Target;

        public bool isActive => _IsActive;

        public Sprite icon => _Icon;

        public bool isCopied { get; set; }


        // Tick, to update your action, also in IEnumerator for to set update in Fixed or normal Update. **DO NOT SETACTIVE(FALSE) GAMEOBJECT AS IT WILL STOP ALL TICKS**
        protected abstract IEnumerator Tick();

        // To Start Action and Setup Variables and Tick.
        protected void Begin(CharacterBase target)
        {
            _IsActive = true;

            _Target = target;

            _TickCoroutine = target.StartCoroutine(Tick());
        }

        // To End Action and stop Tick if still running, you have to End action yourself.
        public virtual void End()
        {
            _IsActive = false;

            if(_TickCoroutine != null)
                _Target.StopCoroutine(_TickCoroutine);
        }

        public T CreateCopy<T>() where T : ScriptableObject, ICopyable
        {
            T copy = Instantiate(this) as T;

            copy.isCopied = true;

            return copy;
        }
    }
}