using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterectableArea : MonoBehaviour
{
    [SerializeField] private float _timeToInteract;
    [SerializeField] private Image _progressBar;
    protected Player _player;
    private float _timer;

    private void Start()
    {
        _progressBar.fillAmount = 0;
    }

    private void Update()
    {
        if (!_player || !CheckCondition())
            return;

        _timer += Time.deltaTime;
        UpdateProgressBar();

        if (_timer > _timeToInteract)
        {
            Interact();
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        bool player = other.tag == "Player";
        
        if (player)
        {
            _player = other.GetComponent<Player>();
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        bool player = other.tag == "Player";
        if (player)
        {
            _player = null;
            _timer = 0;
            UpdateProgressBar();
        }
    }

    protected virtual void Interact()
    {

    }

    private void UpdateProgressBar()
    {
        float fill = Mathf.InverseLerp(0, _timeToInteract, _timer);
        _progressBar.fillAmount = fill;
    }

    protected virtual bool CheckCondition()
    {
        return false;
    }

}
