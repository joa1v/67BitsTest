using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private BodyCarrier _bodyCarrier;
    [SerializeField] private Animator _animator;
    [Header("Parameters")]
    [SerializeField] private string _movementParameter;
    [SerializeField] private string _punchParameter;
    [SerializeField] private string _carryingParameter;
    private bool _isMoving;

    private void OnEnable()
    {
        _bodyCarrier.OnBodiesChanged += SetIsCarrying;
    }

    private void OnDisable()
    {
        _bodyCarrier.OnBodiesChanged -= SetIsCarrying;
    }
    public void MovementInput(InputAction.CallbackContext context)
    {
        _isMoving = context.performed;
        SetMovementAnimations();
    }

    private void SetMovementAnimations()
    {
        _animator.SetBool(_movementParameter, _isMoving);
    }

    public void Punch()
    {
        _animator.SetTrigger(_punchParameter);
    }

    public void SetIsCarrying(int bodiesCount)
    {
        bool isCarrying = bodiesCount > 0;
        _animator.SetBool(_carryingParameter, isCarrying);
    }
}
