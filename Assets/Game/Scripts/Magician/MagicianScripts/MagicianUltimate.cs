using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    [CreateAssetMenu(menuName = "Data/MagicianUltimate")]
    public class MagicianUltimate : Ultimate
    {
        protected override IEnumerator Tick()
        {
            yield return new WaitForEndOfFrame();

            End();
        }

        protected override void OnCooldown()
        {
            Debug.Log(currentCoolDownTime);
        }
    }
}
