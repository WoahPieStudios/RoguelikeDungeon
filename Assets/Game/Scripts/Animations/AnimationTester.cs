using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Animations;

namespace Game.Animations
{
    public class AnimationTester : MonoBehaviour
    {
        [SerializeField]
        private AnimationData _SpinData;
        [SerializeField]
        private AnimationData _OrbitData;
        [SerializeField]
        private AnimationData _CounterOrbitData;
        [SerializeField]
        private AnimationData _SpinAndOrbitData;

        private AnimationHandler _AnimationHandler;

        private void Awake() 
        {
            _AnimationHandler = GetComponent<AnimationHandler>();
        }

        private void Start() 
        {
            _AnimationHandler.AddAnimationData(_SpinData, test);
            _AnimationHandler.AddAnimationData(_OrbitData);
            _AnimationHandler.AddAnimationData(_CounterOrbitData, test, test);
            _AnimationHandler.AddAnimationData(_SpinAndOrbitData);
        }

        private void Update() 
        {
            if(Input.GetKeyDown(KeyCode.J))
                _AnimationHandler.Play(_SpinData);

            if(Input.GetKeyDown(KeyCode.K))
                _AnimationHandler.Play(_OrbitData);

            if(Input.GetKeyDown(KeyCode.L))
                _AnimationHandler.CrossFadePlay(_CounterOrbitData, .1f); 

            if(Input.GetKeyDown(KeyCode.N))
                _AnimationHandler.Play(_SpinAndOrbitData);

            if(Input.GetKeyDown(KeyCode.Semicolon))
                _AnimationHandler.SyncAnimations(_SpinData, _OrbitData);
        }

        private void test()
        {
            Debug.Log("tset");
        }
    }
}