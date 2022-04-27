using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Effects;
using Game.Animations;
using Game.Characters.Actions;


namespace Game.Characters
{
    [RequireComponent(typeof(AnimationHandler), typeof(AudioSource))]
    public abstract class Character : MonoBehaviour, IEffectable, IRestrictableActionsHandler
    {

        EffectsHandler _EffectsHandler = new EffectsHandler();
        IOrientationAction _Orientation;

        AnimationHandler _AnimationHandler;

        AudioSource _AudioSource;

        RestrictableActionsHandler _RestrictableActionsHandler = new RestrictableActionsHandler();
        
        protected RestrictableActionsHandler restrictableActionsHandler => _RestrictableActionsHandler;
        
        public AnimationHandler animationHandler => _AnimationHandler;

        public AudioSource audioSource => _AudioSource;

        public IOrientationAction orientation => _Orientation;

        public Effect[] effects => _EffectsHandler.effects;

        public RestrictActionType restrictedActions => _RestrictableActionsHandler.restrictedActions;

        public event Action<Effect[]> onAddEffectsEvent;
        public event Action<Effect[]> onRemoveEffectsEvent;

        static List<Character> _CharacterList = new List<Character>();

        public static Character[] characters => _CharacterList.ToArray();

        protected virtual void Awake()
        {
            _AnimationHandler = GetComponent<AnimationHandler>();

            _AudioSource = GetComponent<AudioSource>();

            _Orientation = GetComponent<IOrientationAction>();

            restrictableActionsHandler.AddRestrictableAction(GetComponents<IRestrictableAction>());

            _EffectsHandler.onAddEffectsEvent += OnAddEffects;
            _EffectsHandler.onRemoveEffectsEvent += OnRemoveEffects;
        }

        protected virtual void OnEnable()
        {
            _CharacterList.Add(this);
        }

        protected virtual void OnDisable() 
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