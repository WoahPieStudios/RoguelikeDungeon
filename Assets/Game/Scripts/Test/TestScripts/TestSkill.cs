using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    [CreateAssetMenu(menuName = "Data/TestSkill")]
    public class TestSkill : Skill
    {
        public override void OnCooldown()
        {
            Debug.Log(currentCoolDownTime);
        }

        public override IEnumerator Tick()
        {
            yield return new WaitForEndOfFrame();

            End();
        }
    }
}