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

        IEnumerable<PassiveEffect> _AttackPassives;
        IEnumerable<PassiveEffect> _SkillPassives;
        IEnumerable<PassiveEffect> _UltimatePassives;

        Skill _Skill;

        Ultimate _Ultimate;

        // Skill
        public Skill skill => _Skill;

        // Ultimate
        public Ultimate ultimate => _Ultimate;

        // Mana
        public int currentMana => _CurrentMana;
        public int maxMana => data.maxMana;

        // Passives
        public Dictionary<TrackAction, PassiveEffect[]> trackActionPassiveEffects => _TrackActionPassiveEffects;
        public PassiveEffect[] passiveEffects => data.passiveEffects;

        void SetupPassives(IEnumerable<PassiveEffect> passiveEffects)
        {
            _AttackPassives = passiveEffects.Where(passives => passives.trackAction.HasFlag(TrackAction.Attack));
            _SkillPassives = passiveEffects.Where(passives => passives.trackAction.HasFlag(TrackAction.Skill));
            _UltimatePassives = passiveEffects.Where(passives => passives.trackAction.HasFlag(TrackAction.Ultimate));
        }

        // Mana
        public virtual void AddMana(int mana)
        {
            int newMana = _CurrentMana + mana;

            _CurrentMana = newMana > maxMana ? maxMana : newMana;
        }

        public virtual void UseMana(int mana)
        {
            int newMana = _CurrentMana - mana;

            _CurrentMana = newMana > 0 ? newMana : 0;
        }

        // Attack
        public override bool UseAttack()
        {
            bool canAttack = base.UseAttack();

            if(canAttack)
            {
                if(_AttackPassives.Any())
                    AddEffects(this, _AttackPassives.Where(passiveEffect => passiveEffect.CanUse(this)).ToArray());
                // if(_TrackActionPassiveEffects.ContainsKey(TrackAction.Attack))
                //     AddEffects(_TrackActionPassiveEffects[TrackAction.Attack].Where(passiveEffect => passiveEffect.CanUse(this)).ToArray());
            }

            return canAttack;
        }

        // Skill
        public virtual bool UseSkill()
        {
            bool canUse = _Skill && _Skill.CanUse(this) && !restrictedActions.HasFlag(RestrictAction.Skill);

            if(canUse)
            {
                _Skill.Use(this);

                if(_SkillPassives.Any())
                    AddEffects(this, _SkillPassives.Where(passiveEffect => passiveEffect.CanUse(this)).ToArray());
                // if(_TrackActionPassiveEffects.ContainsKey(TrackAction.Skill))
                //     AddEffects(_TrackActionPassiveEffects[TrackAction.Skill].Where(passiveEffect => passiveEffect.CanUse(this)).ToArray());
            }

            return canUse;
        }

        // Ultimate
        public virtual bool UseUltimate()
        {
            bool canUse = _Ultimate && _Ultimate.CanUse(this) && !restrictedActions.HasFlag(RestrictAction.Ultimate);

            if(canUse)
            {
                _Ultimate.Activate(this);
                
                if(_UltimatePassives.Any())
                    AddEffects(this, _UltimatePassives.Where(passiveEffect => passiveEffect.CanUse(this)).ToArray());
                // if(_TrackActionPassiveEffects.ContainsKey(TrackAction.Ultimate))
                //     AddEffects(_TrackActionPassiveEffects[TrackAction.Ultimate].Where(passiveEffect => passiveEffect.CanUse(this)).ToArray());
            }

            return canUse;
        }

        public override void AssignData(HeroData data)
        {
            base.AssignData(data);

            SetupPassives(data.passiveEffects.Select(passive => passive.CreateCopy<PassiveEffect>()));

            _CurrentMana = data.maxMana;

            _Skill = data.skill.CreateCopy<Skill>();

            _Ultimate = data.ultimate.CreateCopy<Ultimate>();
        }
    }
}