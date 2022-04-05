using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters.Effects
{
    public class EffectsHandler
    {
        List<Effect> _EffectList = new List<Effect>();

        /// <summary>
        /// Effects casted upon the Character.
        /// </summary>
        /// <returns></returns>
        public Effect[] effects => _EffectList.ToArray();

        public event System.Action<Effect[]> onAddEffectsEvent;
        public event System.Action<Effect[]> onRemoveEffectsEvent;

        void ProcessStackableEffect(IStackableEffect effect, IEnumerable<Effect> effects)
        {
            if(effects.Any()) // If there is already a copy in the list, expected there is always 1 or none in the least.
                effect.Stack(effects.ToArray());
        }

        void ProcessOtherEffect(IEnumerable<Effect> effects)
        {
            foreach(Effect e in effects)
                e.End();
        }

        IEnumerable<Effect> GetEffectClones(Effect[] effects)
        {
            Effect effect;

            IEnumerable<Effect> effectsInList;

            foreach(IGrouping<int, Effect> effectGroup in effects.GroupBy(effect => effect.instanceId))
            {
                effect = effectGroup.First();
                effect = effect.isClone ? effect : effect.CreateClone();

                effectsInList = _EffectList.Where(e => e.instanceId == effectGroup.Key);

                if(effect is IStackableEffect)
                    ProcessStackableEffect(effect as IStackableEffect, effectGroup);

                else if(effectsInList.Any())
                    ProcessOtherEffect(effectsInList);

                yield return effect;
            }
        }
        
        /// <summary>
        /// Adds the effects to this Character.
        /// </summary>
        /// <param name="sender">The one who sent the effect</param>
        /// <param name="effects">The one who is casted upon</param>
        public virtual void AddEffects(IEffectable sender, IEffectable receiver, params Effect[] effects)
        {
            effects = GetEffectClones(effects).ToArray();

            foreach(Effect effect in effects)
            {
                effect.StartEffect(sender, receiver);

                _EffectList.Add(effect);
            }
                
            // Makes sure there are only 1 effect clone of their instance.
            foreach(IGrouping<int, Effect> effectGroup in _EffectList.GroupBy(effect => effect.instanceId))
            {
                Effect[] effectsArray = effectGroup.ToArray();

                for(int i = 1; i < effectsArray.Length; i++)
                    effectsArray[i].End();
            }

            onAddEffectsEvent?.Invoke(effects);
        }

        /// <summary>
        /// Removes the Effect
        /// </summary>
        /// <param name="effects"></param>
        public virtual void RemoveEffects(params Effect[] effects)
        {
            Effect[] removedEffects = _EffectList.Where(e => effects.Contains(e)).ToArray();

            _EffectList.RemoveAll(e => removedEffects.Contains(e));

            onRemoveEffectsEvent?.Invoke(removedEffects);
        }
    }
}