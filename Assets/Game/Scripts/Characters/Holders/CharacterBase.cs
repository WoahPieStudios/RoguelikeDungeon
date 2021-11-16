using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters
{
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class CharacterBase : MonoBehaviour, IHealth, IMove, IEffectable, IDirectional, IAttack
    {
        // Movement
        Vector2 _Velocity;

        // Direction
        Vector2Int _FaceDirection = Vector2Int.right;

        // Effects
        RestrictAction _RestrictedActions;
        List<Effect> _EffectList = new List<Effect>();

        // Collider
        BoxCollider2D _BoxCollider;

        // Health
        /// <summary>
        /// Max Health of the Character
        /// </summary>
        public abstract int maxHealth { get; }
        
        /// <summary>
        /// Current Health of the Character 
        /// </summary>
        public abstract int currentHealth { get; }

        /// <summary>
        /// Checks if the Character is Alive
        /// </summary>
        public abstract bool isAlive { get; }

        // Attack
        /// <summary>
        /// Attack of the Character
        /// </summary>
        public abstract Attack attack { get; }

        // Effects
        /// <summary>
        /// Restricted Actions of the Character
        /// </summary>
        public RestrictAction restrictedActions => _RestrictedActions;

        /// <summary>
        /// Effects casted upon the Character.
        /// </summary>
        /// <returns></returns>
        public Effect[] effects => _EffectList.ToArray();

        // Movement
        /// <summary>
        /// Move Speed of the Character
        /// </summary>
        public abstract float moveSpeed { get; }

        /// <summary>
        /// Velocity of the Character.
        /// </summary>
        public Vector2 velocity => _Velocity;

        // Direction
        /// <summary>
        /// Direction of the Character is currently facing.
        /// </summary>
        public Vector2Int faceDirection => _FaceDirection;

        // Collider
        public BoxCollider2D boxCollider2D => _BoxCollider;

        protected virtual void Awake()
        {
            _BoxCollider = GetComponent<BoxCollider2D>();
        }

        // Health
        /// <summary>
        /// Adds to the Health of the Character
        /// </summary>
        /// <param name="health">Amount to be added</param>
        public abstract void AddHealth(int health);

        /// <summary>
        /// Reduces the Health of the Character
        /// </summary>
        /// <param name="damage">Amount to be reduced the health by</param>
        public abstract void Damage(int damage);

        // Effects
        void UpdateRestrainedActions()
        {
            RestrictAction RestrainedActions = default(RestrictAction);

            // Adds Restrict Actions to RestrainedActions
            foreach(RestrictAction r in _EffectList.Where(effect => effect.GetType() is IRestrainActionEffect).Select(effect => (effect as IRestrainActionEffect).restrictAction))//_EffectList.Where(effect => effect.GetType().IsSubclassOf(typeof(ActiveEffect))).Select(effect => (effect as ActiveEffect).restrictAction))
                RestrainedActions |= r;

            _RestrictedActions = RestrainedActions;
        }

        // Attack
        /// <summary>
        /// Starts the Attack.
        /// </summary>
        /// <returns>if the Attack can be used</returns>
        public abstract bool UseAttack();

        // Movement
        /// <summary>
        /// Moves the Character. **TO BE DECIDED IF THIS STAYS OR NOT**
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Adds the effects to this Character.
        /// </summary>
        /// <param name="sender">The one who sent the effect</param>
        /// <param name="effects">The one who is casted upon</param>
        public virtual void AddEffects(CharacterBase sender, params Effect[] effects)
        {
            Effect effect;
            Effect effectCopy;

            IEnumerable<Effect> effectsInList;
            IEnumerable<Effect> tempEffectList = _EffectList;

            foreach(IGrouping<int, Effect> effectGroup in effects.GroupBy(effect => effect.instanceId))
            {
                effect = effectGroup.First();

                effectsInList = _EffectList.Where(e => e.instanceId == effectGroup.Key);

                effectCopy = effect.isCopied ? effect : effect.CreateCopy<Effect>();

                if(effect.isStackable)
                {
                    if(effectsInList.Any()) // If there is already a copy in the list, expected there is always 1 or none in the least.
                    {
                        effect = effectsInList.First();

                        effect.Stack(effectGroup.Select(e => e.isCopied ? e : e.CreateCopy<Effect>()).ToArray());
                    }

                    else
                    {
                        effectCopy.Stack(effectGroup.Select(e => e.isCopied ? e : e.CreateCopy<Effect>()).ToArray());
                        effectCopy.StartEffect(sender, this);

                        _EffectList.Add(effectCopy);
                    }
                }

                // Removes all same type in effect list and adds new one. "Refreshes" Effect
                else
                {
                    if(effectsInList.Any())
                    {
                        Effect[] effectsArray = effectsInList.ToArray();

                        for(int i = 1; i < effectsArray.Length; i++)
                            effectsArray[i].End();
                    }
                    
                    effectCopy.StartEffect(sender, this);

                    _EffectList.Add(effectCopy);
                }
            }
                
            foreach(IGrouping<int, Effect> effectGroup in _EffectList.GroupBy(effect => effect.instanceId))
            {
                Effect[] effectsArray = effectGroup.ToArray();

                for(int i = 1; i < effectsArray.Length; i++)
                    effectsArray[i].End();
            }

            // IEnumerable<Effect> effectsInList;

            // // I create a copy of the effects otherwise all effects will go haywire with other Characters as they would only reference 1.
            // foreach(IGrouping<int, Effect> group in effects.GroupBy(effect => effect.instanceId))
            // {
            //     Effect effect = group.First();
                
            //     // If effect is stackable
            //     if(effect.isStackable)
            //     {
            //         // Get effects with same class from _EffectList
            //         effectsInList = _EffectList.Where(effect => effect.instanceId == group.Key);

            //         // If there are any effect in the list with the same type, stack them up
            //         if(effectsInList.Any())
            //         {
            //             effect = effectsInList.First();

            //             effect.Stack(group.ToArray());
            //         }

            //         // If not, stack the first one with the rest of the effects then add to the list
            //         else
            //         {
            //             Effect effectCopy = !effect.isCopied ? effect.CreateCopy<Effect>() : effect;
            //             Effect[] effectsArray;
                        
            //             effectCopy.StartEffect(sender, this);

            //             effectsArray = group.Where(e => e != effect).ToArray();

            //             Debug.LogWarning(effectsInList.ToArray().Length + " " + effectCopy.name);

            //             if(effectsArray.Length > 0)
            //                 effectCopy.Stack(effectsArray);

            //             _EffectList.Add(effectCopy);
            //         }
            //     }

            //     // If not, End current one and add new one
            //     else
            //     {
            //         effectsInList = _EffectList.Where(effect => effect.instanceId == group.Key);

            //         if(effectsInList.Any())
            //         {
            //             Effect tempEffect = effectsInList.First();

            //             if(tempEffect) 
            //                 tempEffect.End();
            //         }

            //         Effect effectCopy = !effect.isCopied ? effect.CreateCopy<Effect>() : effect;

            //         effectCopy.StartEffect(sender, this);
                    
            //         _EffectList.Add(effectCopy);
            //     }
            // }

            UpdateRestrainedActions();
        }

        /// <summary>
        /// Removed the Effect
        /// </summary>
        /// <param name="effects"></param>
        public virtual void RemoveEffects(params Effect[] effects)
        {
            foreach(Effect effect in effects.Where(effect => _EffectList.Contains(effect)))
                _EffectList.Remove(effect);

            UpdateRestrainedActions();
        }

        // Direction
        /// <summary>
        /// Orients the Character to the direction.
        /// </summary>
        /// <param name="faceDirection"></param>
        public virtual void Orient(Vector2Int faceDirection) // Saw it from Jolo's Code
        {
            _FaceDirection = faceDirection;
        }
    }
}