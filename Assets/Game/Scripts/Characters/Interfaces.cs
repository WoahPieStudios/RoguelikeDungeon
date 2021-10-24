using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public interface IIcon
    {
        Sprite icon { get; }
    }

    public interface IUltimate
    {
        bool UseUltimate();
    }

    public interface ISkill
    {
        bool UseSkill();
    }

    public interface IAttack
    {
        bool UseAttack();
    }

    public interface IHealth
    {
        int maxHealth { get; }
        int currentHealth { get; }
        void AddHealth(int health);
        void Damage(int damage);
    }

    public interface IMana
    {
        int maxMana { get; }
        int currentMana { get; }
        void UseMana(int mana);
        void AddMana(int mana);
    }

    public interface IEffectable
    {
        Effect[] effects { get; }
        void AddEffects(params Effect[] effects);
        void RemoveEffects(params Effect[] effects);
    }

    public interface IMove
    {
        Vector2 velocity { get; }
        void Move(Vector2 direction);
    }

    public interface IDirectional
    {
        Vector2Int faceDirection { get; }
        void Orient(Vector2Int faceDirection);
    }
}