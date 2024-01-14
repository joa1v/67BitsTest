using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpArea : InterectableArea
{
    [SerializeField] private int _costToUpgrade;

    protected override void Interact()
    {
        base.Interact();

        _player.PlayerLevel.UpgradeLevel();
        _player.PlayerMoney.RemoveMoney(_costToUpgrade);
    }
    protected override bool CheckCondition()
    {
        bool condition = _player != null && _player.PlayerMoney.Money >= _costToUpgrade;
        return condition;
    }
}
