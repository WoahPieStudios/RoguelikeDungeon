using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters
{
    public class Hero : CharacterBase, IMana, ISkill, IUltimate, IPassiveEffects
    {
        [SerializeField]
        PassiveEffect[] _PassiveEffects;

        #region Mana
        Mana _Mana;
        
        /// <summary>
        /// Current Mana of the Hero
        /// </summary>
        public int currentMana => _Mana.currentMana;

        /// <summary>
        /// Max Mana of the Hero
        /// </summary>
        public int maxMana => _Mana.maxMana;
        
        public event Action<IMana, int> onUseManaEvent;
        public event Action<IMana, int> onAddManaEvent;
        public event System.Action onResetManaEvent;
        public event System.Action onDrainManaEvent;
        #endregion

        #region Skill
        Skill _Skill;
        
        /// <summary>
        /// Skill of the Hero
        /// </summary>
        public Skill skill => _Skill;
        #endregion

        #region Ultimate
        Ultimate _Ultimate;

        // Ultimate
        /// <summary>
        /// Ultimate of the Hero
        /// </summary>
        public Ultimate ultimate => _Ultimate;
        #endregion
        
        #region Passive Effects
        Dictionary<TrackAction, PassiveEffect[]> _TrackActionPassiveEffects = new Dictionary<TrackAction, PassiveEffect[]>();

        // Passives
        /// <summary>
        /// Categorized Passive Effects according to which Action is being tracked
        /// </summary>
        public Dictionary<TrackAction, PassiveEffect[]> trackActionPassiveEffects => _TrackActionPassiveEffects;

        /// <summary>
        /// Passive Effects of the Hero
        /// </summary>
        public PassiveEffect[] passiveEffects => _PassiveEffects;
        #endregion

        #region Unity Functions
        protected override void Awake()
        {
            base.Awake();

            _Mana = GetComponent<Mana>();

            _Skill = GetComponent<Skill>();
            _Skill.IsCast<IOnAssignEvent>()?.OnAssign(this);

            _Ultimate = GetComponent<Ultimate>();
            _Ultimate.IsCast<IOnAssignEvent>()?.OnAssign(this);

            SetupPassives(passiveEffects.Select(passive => passive.CreateClone<PassiveEffect>()));
        }
        #endregion

        #region Attack Functions
        /// <summary>
        /// Starts the Attack.
        /// </summary>
        /// <returns>if the Attack is used</returns>
        public override bool UseAttack()
        {
            bool canAttack = base.UseAttack();

            if(canAttack)
            {
                if(_TrackActionPassiveEffects.ContainsKey(TrackAction.Attack))
                    AddEffects(this, _TrackActionPassiveEffects[TrackAction.Attack].Where(passiveEffect => passiveEffect.CanUse(this)).ToArray());
            }

            return canAttack;
        }

        #endregion

        #region Mana Functions
        /// <summary>
        /// Adds Mana.
        /// </summary>
        /// <param name="mana">Amount added.</param>
        public virtual void AddMana(int mana)
        {
            _Mana.AddMana(mana);

            onAddManaEvent?.Invoke(this, mana);
        }

        /// <summary>
        /// Reduces the Mana.
        /// </summary>
        /// <param name="mana">Amount reduced</param>
        public virtual void UseMana(int mana)
        {
            _Mana.UseMana(mana);

            if(currentMana > 0)
                onUseManaEvent?.Invoke(this, mana);
            else
                onDrainManaEvent?.Invoke();
        }
        
        public void ResetMana()
        {
            _Mana.ResetMana();

            onResetManaEvent?.Invoke();
        }
        
        public void DrainMana()
        {
            _Mana.DrainMana();

            onDrainManaEvent?.Invoke();
        }
        #endregion

        #region Skill Functions
        /// <summary>
        /// Starts the Skill.
        /// </summary>
        /// <returns>if the Skill is used</returns>
        public virtual bool UseSkill()
        {
            bool canUse = _Skill && _Skill.CanUse(this) && !restrictedActions.HasFlag(RestrictAction.Skill);

            if(canUse)
            {
                _Skill.Use(this);
                
                if(_TrackActionPassiveEffects.ContainsKey(TrackAction.Skill))
                    AddEffects(this, _TrackActionPassiveEffects[TrackAction.Skill].Where(passiveEffect => passiveEffect.CanUse(this)).ToArray());
            }

            return canUse;
        }

        #endregion

        #region Ultimate Functions
        /// <summary>
        /// Starts the Ultimate.
        /// </summary>
        /// <returns>if the Ultimate is used</returns>
        public virtual bool UseUltimate()
        {
            bool canUse = _Ultimate && _Ultimate.CanUse(this) && !restrictedActions.HasFlag(RestrictAction.Ultimate);

            if(canUse)
            {
                _Ultimate.Activate(this);
                
                if(_TrackActionPassiveEffects.ContainsKey(TrackAction.Ultimate))
                    AddEffects(this, _TrackActionPassiveEffects[TrackAction.Ultimate].Where(passiveEffect => passiveEffect.CanUse(this)).ToArray());
            }

            return canUse;
        }
        #endregion
    
        #region Passive Effects Functions
        void SetupPassives(IEnumerable<PassiveEffect> passiveEffects)
        {
            foreach(PassiveEffect passiveEffect in passiveEffects)
                passiveEffects.IsCast<IOnAssignEvent>()?.OnAssign(this);

            foreach(Enum e in Enum.GetValues(typeof(TrackAction)) )
                _TrackActionPassiveEffects.Add((TrackAction)e, passiveEffects.Where(p => p.trackAction.HasFlag(e)).ToArray());
        }
        #endregion
    }
}