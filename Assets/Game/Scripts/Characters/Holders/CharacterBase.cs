using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Game.Characters.Animations;
using System;

namespace Game.Characters
{
    public class CharacterBase : MonoBehaviour, IHealth, IAttack, IEffectable, IMovement, IOrientation, IAnimations
    {
        [SerializeField]
        Health _Health;

        #region Health
        public event System.Action<IHealth, int> onAddHealthEvent;
        public event System.Action<IHealth, int> onDamageEvent;
        public event System.Action onKillEvent;
        public event System.Action onResetHealthEvent;

        public int maxHealth => _Health.maxHealth;

        public int currentHealth => _Health.currentHealth;

        public bool isAlive => _Health.isAlive;
        #endregion

        #region Move
        Movement _Movement;
        
        public float moveSpeed => _Movement.moveSpeed;

        public Vector2 velocity => _Movement.velocity;
        #endregion

        #region Animations
        AnimationController _AnimationController;
        #endregion

        #region Attack
        Attack _Attack;

        // Attack
        /// <summary>
        /// Attack of the Character
        /// </summary>
        public Attack attack => _Attack;
        #endregion

        #region Orientation
        Orientation _Orientation;

        public Vector2Int currentDirection => _Orientation.currentDirection;
        #endregion

        #region Effectable
        EffectsHandler _EffectsHandler = new EffectsHandler();
        
        public RestrictAction restrictedActions => _EffectsHandler.restrictedActions;

        public Effect[] effects => _EffectsHandler.effects;
        #endregion

        #region Characters
        static List<CharacterBase> _CharacterList = new List<CharacterBase>();

        public static CharacterBase[] characters => _CharacterList.ToArray();
        #endregion

        #region Unity Functions
        protected virtual void Awake()
        {
            _Health.ResetHealth();
            
            _Orientation = GetComponent<Orientation>();

            _Movement = GetComponent<Movement>();

            _Attack = GetComponent<Attack>();
            _Attack.IsCast<IOnAssignEvent>()?.OnAssign(this);

            _AnimationController = GetComponent<AnimationController>();

            _CharacterList.Add(this);
        }

        protected virtual void OnDestroy() 
        {
            _CharacterList.Remove(this);
        }
        #endregion

        #region Move Functions
        // Movement
        /// <summary>
        /// Moves the Character.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public virtual bool Move(Vector2 direction)
        {
            bool canMove = !_EffectsHandler.restrictedActions.HasFlag(RestrictAction.Movement); 
            
            // Checks first if this is restricted
            if(canMove)
                _Movement.Move(direction);

            return canMove;
        }
        #endregion
    
        #region Attack Functions
        // Attack
        /// <summary>
        /// Starts the Attack.
        /// </summary>
        /// <returns>if the Attack can be used</returns>
        public virtual bool UseAttack()
        {
            bool canAttack = attack && attack.CanUse(this) && !_EffectsHandler.restrictedActions.HasFlag(RestrictAction.Attack);

            if(canAttack)
                attack.Use(this);

            return canAttack;
        }
        #endregion

        #region Effectable Functions
        public void AddEffects(CharacterBase sender, params Effect[] effects)
        {
            _EffectsHandler.AddEffects(sender, this);
        }

        public void RemoveEffects(params Effect[] effects)
        {
            _EffectsHandler.RemoveEffects(effects);
        }
        #endregion
        
        #region Health Functions
        public void AddHealth(int health)
        {
            _Health.AddHealth(health);

            onAddHealthEvent?.Invoke(this, health);
        }

        public void Damage(int damage)
        {
            _Health.Damage(damage);

            if(isAlive)
                onDamageEvent?.Invoke(this, damage);
            
            else
                onKillEvent?.Invoke();
        }

        public void Kill()
        {
            _Health.Kill();

            onKillEvent?.Invoke();
        }
         
        public void ResetHealth()
        {
            _Health.ResetHealth();
            
            onResetHealthEvent?.Invoke();
        }
        #endregion

        #region Orientation Functions
        public void FaceDirection(Vector2Int faceDirection)
        {
            _Orientation.FaceDirection(faceDirection);
        }
        #endregion

        #region Animations Functions
        public void AddAnimation(string name, AnimationClip animationClip, params System.Action[] animationEvents)
        {
            _AnimationController.AddAnimation(name, animationClip, animationEvents);
        }

        public void RemoveAnimation(string name)
        {
            _AnimationController.RemoveAnimation(name);
        }

        public void Play(string name)
        {
            _AnimationController.Play(name);
        }

        public void Stop()
        {
            _AnimationController.Stop();
        }

        public void Stop(string name)
        {
            _AnimationController.Stop(name);
        }

        public void CrossFadePlay(string name, float fadeTime)
        {
            _AnimationController.CrossFadePlay(name, fadeTime);
        }
        #endregion
    }
}