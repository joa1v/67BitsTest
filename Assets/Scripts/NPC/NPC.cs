using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private RagdollCharacter _ragdoll;
    [SerializeField] private Transform _skinRoot;

    [SerializeField] private float _timeToSetKnockedDown;

    private bool _isKnockedDown;
    private bool _isBeingCarried;
    public bool IsKnockedDown { get => _isKnockedDown; set => _isKnockedDown = value; }
    public bool IsBeingCarried { get => _isBeingCarried; set => _isBeingCarried = value; }

    public void DisableRagdoll()
    {
        _skinRoot.transform.localPosition = Vector3.zero;
        _ragdoll.SetRagdoll(false);
    }

    public void Punch(Vector3 direction, float punchForce)
    {
        _ragdoll.SetRagdoll(true);
        _ragdoll.AddForceToRagdoll(direction, punchForce);
        StartCoroutine(SetKnockedDown());
    }

    private IEnumerator SetKnockedDown()
    {
        yield return new WaitForSeconds(_timeToSetKnockedDown);
        _isKnockedDown = true;
    }
}
