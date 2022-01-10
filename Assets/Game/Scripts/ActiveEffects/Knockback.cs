using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.ActiveEffects
{
    [CreatableAsset]
    public class Knockback : ActiveEffect
    {
        [SerializeField]
        float _KnockbackDistance;
        [SerializeField]
        float _KnockbackTime;
        [SerializeField]
        AnimationCurve _KnockbackCurve;
        Coroutine _TickCoroutine;

        IEnumerator Tick()
        {
            float currentTime = 0;

            Vector3 direction = target.transform.position - sender.transform.position;

            Vector3 newPosition = target.transform.position + direction * _KnockbackDistance;

            while(currentTime < _KnockbackTime)
            {
                currentTime += Time.fixedDeltaTime;

                float c = _KnockbackCurve.Evaluate(currentTime / _KnockbackTime);

                target.transform.position = Vector3.Lerp(target.transform.position, newPosition, c);

                yield return new WaitForFixedUpdate();
            }

            End();
        }

        public override void Stack(params Effect[] effects)
        {
            
        }

        public override void StartEffect(CharacterBase sender, CharacterBase effected)
        {
            base.StartEffect(sender, effected);

            Debug.Log(effected);

            _TickCoroutine = effected.StartCoroutine(Tick());
        }

        public override void End()
        {
            base.End();

            target.StopCoroutine(_TickCoroutine);
        }
    }
}
