using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollCharacter : MonoBehaviour
{
    [Header("Ragdoll Components")]
    [SerializeField] private Collider[] _ragdollColliders;
    [SerializeField] private Rigidbody[] _ragdollRigidBodies;
    [SerializeField] private Rigidbody _rigidbodyToAddForce;
    [Header("Default Components")]
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        SetRagdoll(false);
    }

    public void SetRagdoll(bool set)
    {
        SetRagdollRigidbodies(set);

        if (set)
            SetDefaultComponents(!set);
    }

    private void SetRagdollRigidbodies(bool set)
    {
        foreach (var rigidbodies in _ragdollRigidBodies)
        {
            rigidbodies.isKinematic = !set;
        }
    }

    private void SetDefaultComponents(bool set)
    {
        _animator.enabled = set;
    }

    public void AddForceToRagdoll(Vector3 direction, float force)
    {
        _rigidbodyToAddForce.AddForce(direction.normalized * force);
    }
}
