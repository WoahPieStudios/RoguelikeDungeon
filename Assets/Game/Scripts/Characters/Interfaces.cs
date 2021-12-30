using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    // I honestly have no idea why I added lots of interfaces but yeah~ I just added the not forgetting so that it sounds like there's a purpose
    public interface ICopyable
    {
        int instanceId { get; }
        bool isCopied { get; }
        T CreateCopy<T>() where T : Action;
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
    public interface IUltimateUser
    {
        Ultimate ultimate { get; }
        bool UseUltimate();
    }

    // Interface for not forgetting Skill
    public interface ISkillUser
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
        void AddHealth(int health);
        void Damage(int damage);
    }

    // Interface for not forgetting Mana
    public interface IManaUser
    {
        int maxMana { get; }
        int currentMana { get; }
        void UseMana(int mana);
        void AddMana(int mana);
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
    public interface IMove
    {
        float moveSpeed { get; }
        Vector2 velocity { get; }
        bool Move(Vector2 direction);
    }

    // Interface for not forgetting Face Directions
    public interface IDirectional
    {
        Vector2Int faceDirection { get; }
        void Orient(Vector2Int faceDirection); // Saw it from Jolo's Code
    }
}