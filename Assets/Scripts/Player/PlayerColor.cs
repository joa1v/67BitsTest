using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    [SerializeField] private PlayerLevel _playerLevel;
    [SerializeField] private Renderer _playerBodyMesh;
    [SerializeField] private Color[] _levelColors;
    private Material _material;

    private void Awake()
    {
        _material = Instantiate(_playerBodyMesh.material);
        _playerBodyMesh.material = _material;
    }


    private void OnEnable()
    {
        _playerLevel.OnLevelChanged += ChangeColor;
    }

    private void OnDisable()
    {
        _playerLevel.OnLevelChanged -= ChangeColor;
    }

    private void ChangeColor(int level)
    {
        if (level - 1 < _levelColors.Length)
        {
            _material.color = _levelColors[level - 1];
        }
    }
}
