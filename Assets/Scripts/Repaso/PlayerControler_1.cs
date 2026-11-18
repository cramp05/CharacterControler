using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler_1 : MonoBehaviour
{
    CharacterController _controller;
    Animator _animator;

    InputAction _jumpAction;
    InputAction _moveAction;
    Vector2 _moveInput;

    Transform _cameraTransform;

    float _movementSpeed = 12;
    float _jumpForce = 8;

    [SerializeField] float _smoothTime = 0.2f;
    [SerializeField] float _turnSmoothVelocity;

    float _gravity = -15f;
    Vector3 _playerGravity;

    [SerializeField] Transform _sensorPosition;
    [SerializeField] float _sensorRadius;
    [SerializeField] LayerMask _groundLayer;

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();

        _jumpAction = InputSystem.actions["Jump"];
        _moveAction = InputSystem.actions["Move"];

        _cameraTransform = Camera.main.transform;

    }

    void Update()
    {
        _moveInput = _moveAction.ReadValue<Vector2>();

        if(_jumpAction.WasPressedThisFrame() && IsGrouned())
        {
            Jump();
        }
        _animator.SetBool("IsGrouned", IsGrouned());

        Movement();

        Gravity();
    }

    void Movement()
    {
        Vector3 direction = new Vector3(_moveInput.x, 0, _moveInput.y);

        _animator.SetFloat("Horizontal", _moveInput.x);
        _animator.SetFloat("Vertical", _moveInput.y);

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cameraTransform.eulerAngles.y;
        float smothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _cameraTransform.eulerAngles.y, ref _turnSmoothVelocity, _smoothTime);

        transform.rotation = Quaternion.Euler(0, smothAngle, 0);

        if(direction != Vector3.zero)
        {
            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            _controller.Move(moveDirection * _movementSpeed * Time.deltaTime);
        }
    }

    void Gravity()
    {
        if(!IsGrouned())
        {
            _playerGravity.y += _gravity * Time.deltaTime;
        }

        if(IsGrouned() && _playerGravity.y < 0)
        {
            _playerGravity.y = _gravity;
        }

        _controller.Move(_playerGravity * Time.deltaTime);
    }

    void Jump()
    {
        _playerGravity.y = _jumpForce;
    }

    bool IsGrouned()
    {
        return Physics.CheckSphere(_sensorPosition.position, _sensorRadius, _groundLayer);
    }
}
