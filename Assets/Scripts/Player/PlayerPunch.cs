using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    [SerializeField] private PlayerAnimations _animations;
    [SerializeField] private float _punchForce;

    [SerializeField] private float _punchCooldown;
    private float _lastPunchTime;
    private bool _canPunch = true;

    private void Update()
    {
        if (_canPunch)
            return;

        if (Time.time > _lastPunchTime + _punchCooldown && !_canPunch)
        {
            _canPunch = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        NPC npc = other.transform.GetComponentInParent<NPC>();
        if (npc != null && !npc.IsKnockedDown && _canPunch)
        {
            Punch(npc);
        }
    }

    private void Punch(NPC npc)
    {
        _animations.Punch();
        npc.Punch(transform.forward ,_punchForce);
        _canPunch = false;
        _lastPunchTime = Time.time;
    }

}
