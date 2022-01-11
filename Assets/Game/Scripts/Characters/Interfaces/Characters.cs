using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.Interfaces
{
    public interface IAnimations
    {
        void AddAnimation(string name, AnimationClip animationClip, params System.Action[] animationEvents);
        void RemoveAnimation(string name);
        void Play(string name);
        void Stop();
        void Stop(string name);
        void CrossFadePlay(string name, float fadeTime);
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