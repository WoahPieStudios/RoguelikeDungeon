using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Actions;
using Game.Characters.Effects;
using Game.Characters.Properties;


namespace Game.Characters
{
    public class Character : MonoBehaviour, IEffectable, IRestrictableActionsHandler
    {
        [SerializeField]
        Health _Health;

        EffectsHandler _EffectsHandler = new EffectsHandler();
        Movement _Movement;
        Orientation _Orientation;

        RestrictableActionsHandler _RestrictableActionsHandler = new RestrictableActionsHandler();
        
        protected RestrictableActionsHandler restrictableActionsHandler => _RestrictableActionsHandler;

        public Health health => _Health;

        public Movement movement => _Movement;
        public Orientation orientation => _Orientation;

        public Effect[] effects => _EffectsHandler.effects;

        public RestrictActionType restrictedActions => _RestrictableActionsHandler.restrictedActions;

        public event System.Action<Effect[]> onAddEffectsEvent;
        public event System.Action<Effect[]> onRemoveEffectsEvent;

        static List<Character> _CharacterList = new List<Character>();

        public static Character[] characters => _CharacterList.ToArray();

        protected virtual void Awake()
        {
            _CharacterList.Add(this);

            _Health.SetCurrentHealthWithoutEvent(_Health.maxHealth);

            _Movement = GetComponent<Movement>();
            _Orientation = GetComponent<Orientation>();

            restrictableActionsHandler.AddRestrictableAction(GetComponents<IRestrictableAction>());

            _EffectsHandler.onAddEffectsEvent += OnAddEffects;
            _EffectsHandler.onRemoveEffectsEvent += OnRemoveEffects;
        }

        protected virtual void OnDestroy() 
        {
            _CharacterList.Remove(this);
        }
        
        void OnAddEffects(Effect[] addedEffects)
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

        void OnRemoveEffects(Effect[] removedEffects)
        {
            _RestrictableActionsHandler.GetRestrainedActions(effects.Where(e => e is IActionRestricter).Cast<IActionRestricter>());
            _RestrictableActionsHandler.SendRestrictionsToActions();
            
            onRemoveEffectsEvent?.Invoke(removedEffects);
            
            foreach(MonoBehaviour monobehaviour in removedEffects.Where(e => e is MonoBehaviour).Cast<MonoBehaviour>())
                Destroy(monobehaviour.gameObject);
        }

        public void AddEffects(IEffectable sender, params Effect[] effects)
        {
            _EffectsHandler.AddEffects(sender, this, effects);
        }

        public void RemoveEffects(params Effect[] effects)
        {
            _EffectsHandler.RemoveEffects(effects);
        }
    }
}