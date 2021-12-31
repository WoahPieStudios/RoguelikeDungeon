using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters
{
    public class Hero : Character<HeroData>, ISkillUser, IUltimateUser, IPassiveEffects
    {
        int _CurrentMana;
        
        Dictionary<TrackAction, PassiveEffect[]> _TrackActionPassiveEffects = new Dictionary<TrackAction, PassiveEffect[]>();

        Skill _Skill;

        Ultimate _Ultimate;

        // Skill
        /// <summary>
        /// Skill of the Hero
        /// </summary>
        public Skill skill => _Skill;

        // Ultimate
        /// <summary>
        /// Ultimate of the Hero
        /// </summary>
        public Ultimate ultimate => _Ultimate;

        // Mana
        /// <summary>
        /// Current Mana of the Hero
        /// </summary>
        public int currentMana => _CurrentMana;

        /// <summary>
        /// Max Mana of the Hero
        /// </summary>
        public int maxMana => data.maxMana;

        // Passives
        /// <summary>
        /// Categorized Passive Effects according to which Action is being tracked
        /// </summary>
        public Dictionary<TrackAction, PassiveEffect[]> trackActionPassiveEffects => _TrackActionPassiveEffects;

        /// <summary>
        /// Passive Effects of the Hero
        /// </summary>
        public PassiveEffect[] passiveEffects => data.passiveEffects;

        void SetupPassives(IEnumerable<PassiveEffect> passiveEffects)
        {
            foreach(PassiveEffect passiveEffect in passiveEffects)
                passiveEffects.IsCast<IOnAssignEvent>()?.OnAssign();

            foreach(Enum e in Enum.GetValues(typeof(TrackAction)) )
                _TrackActionPassiveEffects.Add((TrackAction)e, passiveEffects.Where(p => p.trackAction.HasFlag(e)).ToArray());
        }

        // Mana
        /// <summary>
        /// Adds Mana.
        /// </summary>
        /// <param name="mana">Amount added.</param>
        public virtual void AddMana(int mana)
        {
            int newMana = _CurrentMana + mana;

            _CurrentMana = newMana > maxMana ? maxMana : newMana;
        }

        /// <summary>
        /// Reduces the Mana.
        /// </summary>
        /// <param name="mana">Amount reduced</param>
        public virtual void UseMana(int mana)
        {
            int newMana = _CurrentMana - mana;

            _CurrentMana = newMana > 0 ? newMana : 0;
        }

        // Attack
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

        // Skill
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

        // Ultimate
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
        
        // Character Data
        /// <summary>
        /// Assigns the Data of the Hero and setups up their corresponding variables.
        /// </summary>
        /// <param name="data">Data of the Hero</param>
        public override void AssignData(HeroData data)
        {
            base.AssignData(data);

            SetupPassives(data.passiveEffects.Select(passive => passive.CreateCopy<PassiveEffect>()));

            _CurrentMana = data.maxMana;

            _Skill = data.skill.CreateCopy<Skill>();
            _Skill.IsCast<IOnAssignEvent>()?.OnAssign();

            _Ultimate = data.ultimate.CreateCopy<Ultimate>();
            _Ultimate.IsCast<IOnAssignEvent>()?.OnAssign();
        }
    }
}