using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Actions;
using Game.Characters.Actions;
using Game.Characters.Effects;
using Game.Characters.Animations;


namespace Game.Characters
{
    public class Character : Actor, IEffectable, IRestrictableActionsHandler
    {
        RestrictActionType _RestrictedActions;
        EffectsHandler _EffectsHandler = new EffectsHandler();
        IMovementAction _Movement;
        IOrientationAction _Orientation;

        List<IRestrictableAction> _RestrictableActionsList = new List<IRestrictableAction>();

        public IMovementAction movement => _Movement;
        public IOrientationAction orientation => _Orientation;

        public RestrictActionType restrictedActions => _RestrictedActions;
        public IEffect[] effects => _EffectsHandler.effects;

        public event Action<IEffect[]> onAddEffectsEvent;
        public event Action<IEffect[]> onRemoveEffectsEvent;

        static List<Character> _CharacterList = new List<Character>();

        public static Character[] characters => _CharacterList.ToArray();

        protected override void Awake()
        {
            base.Awake();

            _CharacterList.Add(this);

            _Movement = GetProperty<Movement>();
            _Orientation = GetProperty<Orientation>();

            AddRestrictable(GetProperties<IRestrictableAction>());

            _EffectsHandler.onAddEffectsEvent += OnAddEffects;
            _EffectsHandler.onRemoveEffectsEvent += OnRemoveEffects;
        }

        protected virtual void OnDestroy() 
        {
            _CharacterList.Remove(this);
        }

        void GetRestrainedActions(IEffect[] effects)
        {
            RestrictActionType restrainedActions = default;

            // Adds Restrict Actions to RestrainedActions
            foreach(RestrictActionType r in effects.Where(effect => effect is IActionRestricter).Select(effect => (effect as IActionRestricter).restrictAction))
                restrainedActions |= r;

            _RestrictedActions = restrainedActions;
        }
        
        void OnAddEffects(IEffect[] addedEffects)
        {
            GetRestrainedActions(effects);

            foreach(MonoBehaviour m in addedEffects.Where(e => e is MonoBehaviour).Cast<MonoBehaviour>())
            {
                m.transform.SetParent(transform, false);
                m.transform.localPosition = Vector3.zero;
            }
            
            foreach(IRestrictableAction restrictable in GetProperties<IRestrictableAction>())
                restrictable.OnRestrict(restrictedActions);

            onAddEffectsEvent?.Invoke(addedEffects);
        }

        void OnRemoveEffects(IEffect[] removedEffects)
        {
            GetRestrainedActions(effects);
            
            onRemoveEffectsEvent?.Invoke(removedEffects);
            
            foreach(MonoBehaviour monobehaviour in removedEffects.Where(e => e is MonoBehaviour).Cast<MonoBehaviour>())
                Destroy(monobehaviour.gameObject);
                
            foreach(IRestrictableAction restrictable in GetProperties<IRestrictableAction>())
                restrictable.OnRestrict(restrictedActions);
        }

        public void AddEffects(IEffectable sender, params IEffect[] effects)
        {
            _EffectsHandler.AddEffects(sender, this, effects);
        }

        public void RemoveEffects(params IEffect[] effects)
        {
            _EffectsHandler.RemoveEffects(effects);
        }

        public void AddRestrictable(params IRestrictableAction[] restrictableActions)
        {
            _RestrictableActionsList.AddRange(restrictableActions.Where(a => !_RestrictableActionsList.Contains(a)));
        }

        public void RemoveRestrictable(params IRestrictableAction[] restrictableActions)
        {
            _RestrictableActionsList.RemoveAll(a => restrictableActions.Contains(a));
        }
    }
}