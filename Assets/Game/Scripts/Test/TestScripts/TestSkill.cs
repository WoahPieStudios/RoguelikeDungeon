using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    [CreatableAsset]
    public class TestSkill : Skill
    {
        protected override IEnumerator Tick()
        {
            yield return new WaitForEndOfFrame();

            End();
        }

        protected override void OnCooldown()
        {
            
        }
    }
}