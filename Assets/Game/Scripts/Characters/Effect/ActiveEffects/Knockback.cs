using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters.Effects
{
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

            Transform receiverTransform = (receiver as MonoBehaviour).transform;

            Vector3 direction = receiverTransform.position - (sender as MonoBehaviour).transform.position;
            Vector3 newPosition = receiverTransform.transform.position + direction * _KnockbackDistance;

            while(currentTime < _KnockbackTime)
            {
                currentTime += Time.fixedDeltaTime;

                float c = _KnockbackCurve.Evaluate(currentTime / _KnockbackTime);

                receiverTransform.position = Vector3.Lerp(receiverTransform.position, newPosition, c);

                yield return new WaitForFixedUpdate();
            }

            End();
        }

        public override void StartEffect(IEffectable sender, IEffectable receiver)
        {
            base.StartEffect(sender, receiver);

            _TickCoroutine = StartCoroutine(Tick());
        }

        public override void End()
        {
            base.End();

            StopCoroutine(_TickCoroutine);
        }
    }
}
