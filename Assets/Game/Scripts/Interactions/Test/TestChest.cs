using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Interactions;
using Game.Parties;
using Game.Upgrades;

public class TestChest : Chest
{
    [SerializeField]
    Sprite _OpenChest;
    [SerializeField]
    UpgradeItem _UpgradeItem;

    SpriteRenderer _SpriteRenderer;

    Func<int, int> test;

    protected override void Awake() 
    {
        base.Awake();
        
        _SpriteRenderer = GetComponent<SpriteRenderer>();

        items = new Game.Items.IItem[] { _UpgradeItem };
    }

    public override void OnInteract(Party party)
    {
        base.OnInteract(party);

        if(!canInteract)
            _SpriteRenderer.sprite = _OpenChest;
    }
}
