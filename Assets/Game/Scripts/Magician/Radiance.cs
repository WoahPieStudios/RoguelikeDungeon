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
    }
}
