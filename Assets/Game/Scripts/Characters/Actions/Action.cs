using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class Action : ScriptableObject, IIcon
    {
        [SerializeField]
        Sprite _Icon;

        bool _IsActive = false;

        Coroutine _TickCoroutine;

        CharacterBase _CharacterBase;

        public bool isActive => _IsActive;

        public Sprite icon => _Icon;

        protected CharacterBase characterBase => _CharacterBase;

        protected void Begin(CharacterBase characterBase)
        {
            _IsActive = true;

            _CharacterBase = characterBase;

            _TickCoroutine = characterBase.StartCoroutine(Tick());
        }

        public virtual void End()
        {
            _IsActive = false;

            if(_TickCoroutine != null)
                _CharacterBase.StopCoroutine(_TickCoroutine);
        }

        public abstract IEnumerator Tick();
    }
}