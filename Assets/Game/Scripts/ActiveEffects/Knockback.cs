using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.ActiveEffects
{
    [CreatableAsset]
    public class Knockback : ActiveEffect
    {
        [SerializeField]
        float _KnockbackForce;
        [SerializeField]
        float _KnockbackTime;
        Coroutine _TickCoroutine;

        IEnumerator Tick()
        {
            float currentTime = _KnockbackTime;

            while(currentTime > 0)
            {
                Vector3 direction = target.transform.position - sender.transform.position;

                currentTime -= Time.fixedDeltaTime;

                target.transform.position += direction * _KnockbackForce * Time.fixedDeltaTime;

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
