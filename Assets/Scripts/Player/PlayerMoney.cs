using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    private int _money;
    public int Money
    {
        get => _money;
        private set
        {
            _money = value;
            OnMoneyChanged?.Invoke(_money);
        }
    }

    public delegate void MoneyChangeEventHandler(int money);
    public event MoneyChangeEventHandler OnMoneyChanged;

    public void AddMoney(int amount)
    {
        Money += amount;
    }

    public void RemoveMoney(int amount)
    {
        Money -= amount;
    }
}
