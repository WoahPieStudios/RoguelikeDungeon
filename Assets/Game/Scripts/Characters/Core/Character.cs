using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Effects;
using Game.Animations;
using Game.Characters.Actions;


namespace Game.Characters
{
    [RequireComponent(typeof(AnimationHandler), typeof(AudioSource))]
    public abstract class Character : MonoBehaviour
    {
        IOrientationAction _Orientation;

        AnimationHandler _AnimationHandler;

        AudioSource _AudioSource;
        
        public AnimationHandler animationHandler => _AnimationHandler;

        public AudioSource audioSource => _AudioSource;

        public IOrientationAction orientation => _Orientation;

        static List<Character> _CharacterList = new List<Character>();

        public static Character[] characters => _CharacterList.ToArray();

        protected virtual void Awake()
        {
            _AnimationHandler = GetComponent<AnimationHandler>();

            _AudioSource = GetComponent<AudioSource>();

            _Orientation = GetComponent<IOrientationAction>();
        }

        protected virtual void OnEnable()
        {
            _CharacterList.Add(this);
        }

        protected virtual void OnDisable() 
        {
            _CharacterList.Remove(this);
        }
    }
}