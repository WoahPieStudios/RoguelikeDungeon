using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Animations;

namespace Game.Characters
{
    // I honestly have no idea why I added lots of interfaces but yeah~ I just added the not forgetting so that it sounds like there's a purpose

    public interface IAnimations
    {
        void AddAnimation(string name, AnimationClip animationClip, params System.Action[] animationEvents);
        void RemoveAnimation(string name);
        void Play(string name);
        void Stop();
        void Stop(string name);
        void CrossFadePlay(string name, float fadeTime);
    }

    public interface IAction<T>
    {
        bool isActive { get; }
        T target { get; }


        void ForceStart();
        void End();
    }
    
    public interface IOnAssignEvent
    {
        void OnAssign(CharacterBase character);
    }

    public interface ICloneable
    {
        int instanceId { get; }
        bool isCopied { get; }
        T CreateClone<T>() where T : Object, ICloneable;
        void InitializeClone(int instanceid);
    }

    public interface IPassiveEffects
    {
        Dictionary<TrackAction, PassiveEffect[]> trackActionPassiveEffects { get; }
        PassiveEffect[] passiveEffects { get; }
    }

    // Interface for not forgetting Cool Down
    public interface ICoolDown
    {
        float coolDownTime { get; }
        float currentCoolDownTime { get; }
        bool isCoolingDown { get; }
    }

    // Interface for not forgetting Track Action
    public interface ITrackActionEffect
    {
        TrackAction trackAction { get; }
    }

    // Interface for not forgetting Stackables
    public interface IRestrainActionEffect
    {
        RestrictAction restrictAction { get; }
    }

    // Interface for not forgetting Stackables
    public interface IStackableEffect
    {
        bool isStackable { get; }

        void Stack(params Effect[] effects);
    }

    // Interface for not forgetting to Icons
    public interface IIcon
    {
        Sprite icon { get; }
    }

    // Interface for not forgetting Ultimate
    public interface IUltimate
    {
        Ultimate ultimate { get; }
        bool UseUltimate();
    }

    // Interface for not forgetting Skill
    public interface ISkill
    {
        Skill skill { get; }
        bool UseSkill();
    }

    // Interface for not forgetting Attack
    public interface IAttack
    {
        Attack attack { get; }
        bool UseAttack();
    }

    // Interface for not forgetting Health
    public interface IHealth
    {
        int maxHealth { get; }
        int currentHealth { get; }
        bool isAlive { get; }

        event System.Action<IHealth, int> onAddHealthEvent;
        event System.Action<IHealth, int> onDamageEvent;
        event System.Action onKillEvent;
        event System.Action onResetHealthEvent;

        void AddHealth(int health);
        void Damage(int damage);
        void Kill();
        void ResetHealth();
    }

    // Interface for not forgetting Mana
    public interface IMana
    {
        int maxMana { get; }
        int currentMana { get; }

        event System.Action<IMana, int> onUseManaEvent;
        event System.Action<IMana, int> onAddManaEvent;
        event System.Action onDrainManaEvent;
        event System.Action onResetManaEvent;

        void UseMana(int mana);
        void AddMana(int mana);
        void DrainMana();
        void ResetMana();

        public delegate void OnUseMana(IMana mana, int usedMana);
    }

    // Interface for not forgetting Effects
    public interface IEffectable
    {
        RestrictAction restrictedActions { get; }
        Effect[] effects { get; }
        void AddEffects(CharacterBase sender, params Effect[] effects);
        void RemoveEffects(params Effect[] effects);
    }

    // Interface for not forgetting Movement
    public interface IMovement
    {
        float moveSpeed { get; }
        Vector2 velocity { get; }
        bool Move(Vector2 direction);
    }

    // Interface for not forgetting Face Directions
    public interface IOrientation
    {
        Vector2Int currentDirection { get; }
        void FaceDirection(Vector2Int faceDirection);
    }
}