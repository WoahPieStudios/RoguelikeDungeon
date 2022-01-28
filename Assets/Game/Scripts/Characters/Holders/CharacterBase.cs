using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Animations;
using Game.Characters.Interfaces;
using System;

namespace Game.Characters
{
    public class CharacterBase : MonoBehaviour, IEffectable, IRestrictableActionsHandler
    {
        [SerializeField]
        Health _Health;

        Attack _Attack;
        IMovementHandler _MovementHandler;
        IAnimationsHandler _AnimationsHandler;
        IOrientationHandler _OrientationHandler;
        EffectsHandler _EffectsHandler = new EffectsHandler();

        List<IRestrictableAction> _RestrictableActionsList = new List<IRestrictableAction>();

        public Health health => _Health;
        public IMovementHandler movement => _MovementHandler;
        public IAnimationsHandler animationHandler => _AnimationsHandler;
        public IOrientationHandler orientation => _OrientationHandler;

        /// <summary>
        /// Attack of the Character
        /// </summary>
/// 
        public IRestrictableAction[] restrictableActions => _RestrictableActionsList.ToArray();
        public Attack attack => _Attack;
        public RestrictAction restrictedActions => _EffectsHandler.restrictedActions;
        public Effect[] effects => _EffectsHandler.effects;

        static List<CharacterBase> _CharacterList = new List<CharacterBase>();

        public event Action<CharacterBase, Effect[]> onAddEffectsEvent;
        public event Action<Effect[]> onRemoveEffectsEvent;

        public static CharacterBase[] characters => _CharacterList.ToArray();

        protected virtual void Awake()
        {
            _OrientationHandler = GetComponent<IOrientationHandler>();

            _MovementHandler = GetComponent<IMovementHandler>();

            _AnimationsHandler = GetComponent<IAnimationsHandler>();

            _Attack = GetComponent<Attack>();
            
            _Health.ResetHealth(false);

            AddRestrictable(GetComponents<IRestrictableAction>());

            _EffectsHandler.onAddEffectsEvent += onAddEffectsEvent;
            _EffectsHandler.onRemoveEffectsEvent += onRemoveEffectsEvent;

            _CharacterList.Add(this);
        }

        protected virtual void OnDestroy() 
        {
            _CharacterList.Remove(this);
        }

        public void AddEffects(CharacterBase sender, params Effect[] effects)
        {
            _EffectsHandler.AddEffects(sender, this, effects);
        }

        public void RemoveEffects(params Effect[] effects)
        {
            _EffectsHandler.RemoveEffects(effects);
        }

        public void AddRestrictable(params IRestrictableAction[] restrictableActions)
        {
            _RestrictableActionsList.AddRange(restrictableActions);
        }

        public void RemoveRestrictable(params IRestrictableAction[] restrictableActions)
        {
            foreach(IRestrictableAction restrictable in restrictableActions.Where(r => _RestrictableActionsList.Contains(r)))
                _RestrictableActionsList.Remove(restrictable);
        }
    }
}