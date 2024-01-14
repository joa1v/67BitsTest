using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyDropArea : InterectableArea
{
    [SerializeField] private int _moneyPerBody = 1;
    [SerializeField] private Transform _bodydropPosition;

    protected override void Interact()
    {
        base.Interact();

        _player.PlayerMoney.AddMoney(_moneyPerBody * _player.BodyCarrier.BodyCount);
        _player.BodyCarrier.DropBodies(_bodydropPosition);
    }

    protected override bool CheckCondition()
    {
        bool condition = _player != null && _player.BodyCarrier.BodyCount > 0;
        return condition;
    }
}
