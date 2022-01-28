using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Interfaces;

namespace Game.Characters
{
    public class Hero : CharacterBase, ITrackableActionsHandler
    {
        [SerializeField]
        Mana _Mana;
        [SerializeField]
        PassiveEffect[] _PassiveEffects;

        TrackAction _TrackAction;
        List<ITrackableAction> _TrackableActionList = new List<ITrackableAction>();

        Dictionary<TrackAction, PassiveEffect[]> _TrackActionPassiveEffects = new Dictionary<TrackAction, PassiveEffect[]>();

        Skill _Skill;
        Ultimate _Ultimate;

        public Mana mana => _Mana;
        
        /// <summary>
        /// Skill of the Hero
        /// </summary>
        public Skill skill => _Skill;

        /// <summary>
        /// Ultimate of the Hero
        /// </summary>
        public Ultimate ultimate => _Ultimate;

        public ITrackableAction[] trackableActions => _TrackableActionList.ToArray();

        public TrackAction trackedActions => _TrackAction;

        protected override void Awake()
        {
            base.Awake();
            
            _Mana.ResetMana();

            _Skill = GetComponent<Skill>();

            _Ultimate = GetComponent<Ultimate>();

            SetupPassives(_PassiveEffects.Select(passive => passive.CreateClone<PassiveEffect>()));

            AddTrackable(GetComponents<ITrackableAction>());

            onAddEffectsEvent += OnAddEffects;
            onRemoveEffectsEvent += OnRemoveEffects;
        }

        void OnAddEffects(CharacterBase sender, Effect[] effects)
        {
            foreach(IRestrictableAction restrictable in restrictableActions)
                restrictable.OnRestrict(restrictedActions);
        }

        void OnRemoveEffects(Effect[] effects)
        {
            foreach(IRestrictableAction restrictable in restrictableActions)
                restrictable.OnRestrict(restrictedActions);
        }
    
        void SetupPassives(IEnumerable<PassiveEffect> passiveEffects)
        {
            foreach(PassiveEffect passiveEffect in passiveEffects)
                passiveEffects.IsCast<IOnAssignEvent>()?.OnAssign(this);

            foreach(Enum e in Enum.GetValues(typeof(TrackAction)))
            {
                TrackAction trackAction = (TrackAction)e;

                PassiveEffect[] sortedPassiveEffects = passiveEffects.Where(p => p.trackAction.HasFlag(e)).ToArray();

                if(sortedPassiveEffects.Any())
                {
                    _TrackAction |= trackAction;

                    _TrackActionPassiveEffects.Add(trackAction, sortedPassiveEffects);
                }
            }
        }

        void OnActionTracked(TrackAction trackAction)
        {
            foreach(Enum e in Enum.GetValues(typeof(TrackAction)))
            {
                if(trackAction.HasFlag((TrackAction)e) && _TrackActionPassiveEffects.ContainsKey(trackAction))
                {
                    PassiveEffect[] passiveEffects = _TrackActionPassiveEffects[trackAction];

                    if(passiveEffects.Any())
                        AddEffects(this, passiveEffects.Where(p => p.CanUse(this)).ToArray());
                }
            }
        }

        public void AddTrackable(params ITrackableAction[] trackables)
        {
            foreach(ITrackableAction trackable in trackables.Where(t => !_TrackableActionList.Contains(t)))
            {
                trackable.onActionTracked += OnActionTracked;

                _TrackableActionList.Add(trackable);
            }
        }

        public void RemoveTrackable(params ITrackableAction[] trackables)
        {
            foreach(ITrackableAction trackable in trackables.Where(t => _TrackableActionList.Contains(t)))
            {
                trackable.onActionTracked -= OnActionTracked;

                _TrackableActionList.Remove(trackable);
            }
        }
    }
}