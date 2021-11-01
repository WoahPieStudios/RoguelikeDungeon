using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;

[CreateAssetMenu(menuName = "Data/TestAttack")]
public class TestAttack : Attack
{
    [SerializeField]
    LayerMask _EnemyLayerMask;
    protected override IEnumerator Tick()
    {
        Debug.Log(target.FaceNearestEnemy(range, _EnemyLayerMask));
        
        yield return null;

        End();
    }
}
