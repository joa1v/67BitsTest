using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _fps;
    [Header("Money")]
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private PlayerMoney _playerMoney;
    [Header("Level")]
    [SerializeField] private PlayerLevel _player;
    [SerializeField] private TextMeshProUGUI _levelText;

    private void OnEnable()
    {
        _playerMoney.OnMoneyChanged += UpdateMoneyText;
        _player.OnLevelChanged += UpdateLevel;
    }

    private void OnDisable()
    {
        _playerMoney.OnMoneyChanged -= UpdateMoneyText;
        _player.OnLevelChanged -= UpdateLevel;
    }

    private void Update()
    {
        _fps.text = (1/Time.deltaTime).ToString("F0");
    }

    private void UpdateMoneyText(int money)
    {
        _moneyText.text = money.ToString();
    }

    private void UpdateLevel(int level)
    {
        _levelText.text = $"Level {level}";
    }
}
