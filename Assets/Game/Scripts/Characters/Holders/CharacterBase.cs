using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters
{
    public abstract class CharacterBase : MonoBehaviour, IHealth, IMove, IEffectable, IDirectional, IAttack
    {
        // Movement
        Vector2 _Velocity;

        // Direction
        Vector2Int _FaceDirection;

        // Effects
        RestrictAction _RestrictedActions;
        List<Effect> _EffectList = new List<Effect>();

       // Health
        public abstract int maxHealth { get; }
        public abstract int currentHealth { get; }
        public abstract bool isAlive { get; }

        // Attack
        public abstract Attack attack { get; }

        // Effects
        public RestrictAction restrictedActions => _RestrictedActions;
        public Effect[] effects => _EffectList.ToArray();

        // Movement
        public abstract float moveSpeed { get; }
        public Vector2 velocity => _Velocity;

        // Direction
        public Vector2Int faceDirection => _FaceDirection;

        // Health
        public abstract void AddHealth(int health);
        public abstract void Damage(int damage);

        // Effects
        void UpdateRestrainedActions()
        {
            RestrictAction RestrainedActions = default(RestrictAction);

            // Adds Restrict Actions to RestrainedActions
            foreach(RestrictAction r in  _EffectList.Where(effect => effect.GetType().IsSubclassOf(typeof(ActiveEffect))).Select(effect => (effect as ActiveEffect).restrictAction))
                RestrainedActions |= r;

            _RestrictedActions = RestrainedActions;
        }

        // Attack
        public abstract bool UseAttack();

        // Movement
        public virtual bool Move(Vector2 direction)
        {
            bool canMove = !_RestrictedActions.HasFlag(RestrictAction.Movement); 
            
            // Checks first if this is restricted
            if(canMove)
            {
                // Definitely expecting this to be replaced or to have its own class
                _Velocity = direction * moveSpeed * Time.fixedDeltaTime;

                transform.position += (Vector3)_Velocity;
            }

            return canMove;
        }

        // Effects
        public virtual void AddEffects(params Effect[] effects)
        {
            Effect effect;
            Effect effectCopy;
            Effect[] effectsArray;
            IEnumerable<Effect> effectsInList;

            // I create a copy of the effects otherwise all effects will go haywire with other Characters as they would only reference 1.
            foreach(IGrouping<System.Type, Effect> group in effects.GroupBy(effect => effect.GetType()))
            {
                effect = group.First();
                
                // If effect is stackable
                if(effect.isStackable)
                {
                    // Get effects with same class from _EffectList
                    effectsInList = _EffectList.Where(effect => effect.GetType() == group.Key);

                    // If there are any effect in the list with the same type, stack them up
                    if(effectsInList.Any())
                        effectsInList.First().Stack(group.ToArray());

                    // If not, stack the first one with the rest of the effects then add to the list
                    else
                    {
                        effectCopy = !effect.isCopied ? effect.CreateCopy<Effect>() : effect;
                        
                        effectCopy.StartEffect(this);

                        effectsArray = group.Where(e => e != effect).ToArray();

                        if(effectsArray.Length > 0)
                            effectCopy.Stack(effectsArray);

                        _EffectList.Add(effectCopy);
                    }
                }

                // If not, add them normally
                else
                {
                    foreach(Effect e in group.Select(effect => !effect.isCopied ? effect.CreateCopy<Effect>() : effect))
                    {
                        effect.StartEffect(this);

                        _EffectList.Add(effect);
                    }
                }
            }

            UpdateRestrainedActions();
        }

        public virtual void RemoveEffects(params Effect[] effects)
        {
            foreach(Effect effect in effects.Where(effect => _EffectList.Contains(effect)))
                _EffectList.Remove(effect);

            UpdateRestrainedActions();
        }

        // Direction
        public virtual void Orient(Vector2Int faceDirection) // Saw it from Jolo's Code
        {
            _FaceDirection = faceDirection;
        }
    }
}