using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    [CreatableAsset("Magician")]
    public class Radiance : Skill
    {
        [Header("Target")]
        [SerializeField]
        LayerMask _EnemyLayer;
        
        [Header("Light Ray")]
        [SerializeField]
        GameObject _Prefab;
        [SerializeField]
        float _FadeInTime;
        [SerializeField]
        float _FadeOutTime;

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
