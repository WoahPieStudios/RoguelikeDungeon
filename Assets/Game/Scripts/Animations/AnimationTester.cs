using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Animations;

namespace Game.Animations
{
    public class AnimationTester : MonoBehaviour
    {
        [SerializeField]
        private AnimationClip _Spin;
        [SerializeField]
        private AnimationClip _Orbit;
        [SerializeField]
        private AnimationClip _CounterOrbit;
        [SerializeField]
        private AnimationClip _SpinAndOrbit;

        private static string _SpinAnimation = "Spin";
        private static string _OrbitAnimation = "Orbit";
        private static string _CounterOrbitAnimation = "CounterOrbit";
        private static string _SpinAndOrbitAnimation = "SpinAndOrbit";

        private AnimationHandler _AnimationHandler;

        private void Awake() 
        {
            _AnimationHandler = new AnimationHandler(GetComponent<Animation>());
            // _AnimationHandler = GetComponent<AnimationController>();
        }

        private void Start() 
        {
            _AnimationHandler.AddAnimation(_SpinAnimation, _Spin, 2);
            _AnimationHandler.AddAnimation(_OrbitAnimation, _Orbit, 1);
            _AnimationHandler.AddAnimation(_CounterOrbitAnimation, _CounterOrbit, 1);
            _AnimationHandler.AddAnimation(_SpinAndOrbitAnimation, _SpinAndOrbit, 0);

            foreach(AnimationState state in _AnimationHandler.GetAnimationStates())
            {
                Debug.Log(state.name + ": " + state.layer);
            }
        }

        private void Update() 
        {
            if(Input.GetKeyDown(KeyCode.J))
                _AnimationHandler.Play(_SpinAnimation);

            if(Input.GetKeyDown(KeyCode.K))
                _AnimationHandler.Play(_OrbitAnimation);

            if(Input.GetKeyDown(KeyCode.L))
                _AnimationHandler.CrossFadePlay(_CounterOrbitAnimation, .1f); 

            if(Input.GetKeyDown(KeyCode.N))
                _AnimationHandler.Play(_SpinAndOrbitAnimation);

            if(Input.GetKeyDown(KeyCode.Semicolon))
                _AnimationHandler.SyncAnimations(_SpinAnimation, _OrbitAnimation);
        }
    }
}