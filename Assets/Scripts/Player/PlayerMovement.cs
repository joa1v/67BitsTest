using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private BodyCarrier _bodyCarrier;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _carryingSpeed;
    private Vector2 _moveVector;

    private float _currentSpeed;

    private void Start()
    {
        _currentSpeed = _movementSpeed;
    }

    private void OnEnable()
    {
        _bodyCarrier.OnBodiesChanged += ChangeMoveSpeed;
    }

    private void OnDisable()
    {
        _bodyCarrier.OnBodiesChanged -= ChangeMoveSpeed;
    }

    public void MovementInput(InputAction.CallbackContext context)
    {
        _moveVector = context.ReadValue<Vector2>().normalized;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movementDirection = new Vector3(_moveVector.x, 0, _moveVector.y);

        if (movementDirection.magnitude > 0)
        {
            _rb.velocity = new Vector3(movementDirection.x * _currentSpeed * Time.deltaTime
                           , _rb.velocity.y,
                           movementDirection.z * _movementSpeed * Time.deltaTime);

            transform.forward = movementDirection;
        }

    }

    private void ChangeMoveSpeed(int bodiesBeingCarried)
    {
        _currentSpeed = bodiesBeingCarried > 0 ? _carryingSpeed : _movementSpeed;
    }
}
