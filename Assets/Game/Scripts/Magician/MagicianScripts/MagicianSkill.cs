using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    [CreatableAsset("Magician")]
    public class MagicianSkill : Skill
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
