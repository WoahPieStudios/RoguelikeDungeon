using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Actions;
using Game.Characters.Actions;
using Game.Characters.Effects;
using Game.Characters.Properties;


namespace Game.Characters
{
    public class Character : Actor, ICharacterActor
    {
        [SerializeField]
        Health _Health;

        EffectsHandler _EffectsHandler = new EffectsHandler();
        IMovementAction _Movement;
        IOrientationAction _Orientation;

        RestrictableActionsHandler _RestrictableActionsHandler = new RestrictableActionsHandler();
        
        protected RestrictableActionsHandler restrictableActionsHandler => _RestrictableActionsHandler;

        public IHealthProperty health => _Health;

        public IMovementAction movement => _Movement;
        public IOrientationAction orientation => _Orientation;

        public IEffect[] effects => _EffectsHandler.effects;

        public RestrictActionType restrictedActions => _RestrictableActionsHandler.restrictedActions;

        public event Action<IEffect[]> onAddEffectsEvent;
        public event Action<IEffect[]> onRemoveEffectsEvent;

        static List<Character> _CharacterList = new List<Character>();

        public static Character[] characters => _CharacterList.ToArray();


        protected override void Awake()
        {
            base.Awake();

            _CharacterList.Add(this);

            _Health.owner = this;
            _Health.ResetHealth();

            _Movement = GetProperty<IMovementAction>();
            _Orientation = GetProperty<IOrientationAction>();

            AddProperty(_Health);

            restrictableActionsHandler.AddRestrictableAction(GetProperties<IRestrictableAction>());

            _EffectsHandler.onAddEffectsEvent += OnAddEffects;
            _EffectsHandler.onRemoveEffectsEvent += OnRemoveEffects;
        }

        protected virtual void OnDestroy() 
        {
            _CharacterList.Remove(this);
        }
        
        void OnAddEffects(IEffect[] addedEffects)
        {
            foreach(MonoBehaviour m in addedEffects.Where(e => e is MonoBehaviour).Cast<MonoBehaviour>())
            {
                m.transform.SetParent(transform, false);
                m.transform.localPosition = Vector3.zero;
            }
            
            _RestrictableActionsHandler.GetRestrainedActions(effects.Where(e => e is IActionRestricter).Cast<IActionRestricter>());
            _RestrictableActionsHandler.SendRestrictionsToActions();

            onAddEffectsEvent?.Invoke(addedEffects);
        }

        void OnRemoveEffects(IEffect[] removedEffects)
        {
            _RestrictableActionsHandler.GetRestrainedActions(effects.Where(e => e is IActionRestricter).Cast<IActionRestricter>());
            _RestrictableActionsHandler.SendRestrictionsToActions();
            
            onRemoveEffectsEvent?.Invoke(removedEffects);
            
            foreach(MonoBehaviour monobehaviour in removedEffects.Where(e => e is MonoBehaviour).Cast<MonoBehaviour>())
                Destroy(monobehaviour.gameObject);
        }

        public void AddEffects(IEffectable sender, params IEffect[] effects)
        {
            _EffectsHandler.AddEffects(sender, this, effects);
        }

        public void RemoveEffects(params IEffect[] effects)
        {
            _EffectsHandler.RemoveEffects(effects);
        }
    }
}