using System.Collections;
using Game.Characters;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Knight/Ultimate")]
public class KnightUltimate : Ultimate
{
    protected override IEnumerator Tick()
    {
        yield return null;
        End();
    }

    protected override void OnCooldown()
    {
        throw new System.NotImplementedException();
    }
}
