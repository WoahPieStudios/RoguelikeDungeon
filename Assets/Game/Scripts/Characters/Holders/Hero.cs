using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters
{
    public class Hero : Character<HeroData>, ISkill, IUltimate
    {
        int _CurrentMana;

        IEnumerable<PassiveEffect> _AttackPassives;
        IEnumerable<PassiveEffect> _SkillPassives;
        IEnumerable<PassiveEffect> _UltimatePassives;

        Skill _Skill;

        Ultimate _Ultimate;

        public int currentMana => _CurrentMana;
        public int maxMana => data.maxMana;

        void SetupPassives(IEnumerable<PassiveEffect> passives)
        {
            _AttackPassives = passives.Where(passives => passives.trackAction.HasFlag(TrackAction.Attack));
            _SkillPassives = passives.Where(passives => passives.trackAction.HasFlag(TrackAction.Skill));
            _UltimatePassives = passives.Where(passives => passives.trackAction.HasFlag(TrackAction.Ultimate));
        }

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

        public override bool UseAttack()
        {
            bool canAttack = base.UseAttack();

            if(canAttack)
            {
                foreach(PassiveEffect passiveEffect in _AttackPassives)
                {
                    if(!passiveEffect.isActive)
                        passiveEffect.Initialize(this);
                }
            }

            return canAttack;
        }

        public virtual bool UseSkill()
        {
            bool canUse = _Skill && _Skill.CanUse();

            if(canUse)
            {
                _Skill.Use(this);

                foreach(PassiveEffect passiveEffect in _SkillPassives)
                {
                    if(!passiveEffect.isActive)
                        passiveEffect.Initialize(this);
                }
            }

            return canUse;
        }

        public virtual bool UseUltimate()
        {
            bool canUse = _Ultimate && _Ultimate.CanUse(this);

            if(canUse)
            {
                _Ultimate.Activate(this);

                foreach(PassiveEffect passiveEffect in _UltimatePassives)
                {
                    if(!passiveEffect.isActive)
                        passiveEffect.Initialize(this);
                }
            }

            return canUse;
        }

        public override void AssignData(HeroData data)
        {
            base.AssignData(data);

            SetupPassives(data.passiveEffects.Select(passive => passive.CreateCopy()));

            _CurrentMana = data.maxMana;

            _Skill = data.skill.CreateCopy();

            _Ultimate = data.ultimate.CreateCopy();
        }
    }
}