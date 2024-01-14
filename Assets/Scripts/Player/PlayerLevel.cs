using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] private int _maxLevel;
    [SerializeField] private BodyCarrier _bodyCarrier;
    private int _level = 1;

    public delegate void LevelChangeEventHandler(int level);
    public event LevelChangeEventHandler OnLevelChanged;

    private void Start()
    {
        ResetLevel();
    }

    public int Level
    {
        get => _level;
        private set
        {
            _level = value;
            OnLevelChanged?.Invoke(_level);
        }

    }

    public void UpgradeLevel()
    {
        if (_level < _maxLevel)
        {
            Level++;
            _bodyCarrier.BodyCarryCapacity++;
        }
    }

    private void ResetLevel()
    {
        Level = 1;
    }
}
