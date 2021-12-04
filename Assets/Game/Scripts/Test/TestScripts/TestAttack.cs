using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;

[CreateAssetMenu(menuName = "Data/TestAttack")]
public class TestAttack : Attack, IReceiveAttackBonus
{
    [SerializeField]
    LayerMask _CharacterLayer;

    List<IAttackBonus> _AttackBonusList = new List<IAttackBonus>();

    public IAttackBonus[] attackBonuses => _AttackBonusList.ToArray();

    public void AddAttackBonus(IAttackBonus attackBonus)
    {
        Debug.Log(attackBonus);
        _AttackBonusList.Add(attackBonus);
    }

    public void RemoveAttackBonus(IAttackBonus attackBonus)
    {
        Debug.Log(attackBonus);
        _AttackBonusList.Remove(attackBonus);
    }

    protected override IEnumerator Tick()
    {
        target.FaceNearestCharacter(range, _CharacterLayer);
        // target.FaceNearestCharacter<CharacterBase>(range, _CharacterLayer);

        // Utilities.GetCharacters(Vector3.zero, 5, _CharacterLayer);
        // Utilities.GetCharacters<CharacterBase>(Vector3.zero, 5, _CharacterLayer);
        // Utilities.GetNearestCharacter<CharacterBase>(Vector3.zero, 5, _CharacterLayer);

        foreach(IAttackBonus bonus in _AttackBonusList)
            Debug.Log(bonus.damageBonus);
        
        yield return null;

        End();
    }
}
