using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    private CharacterController _characterController;

    private InputAction _moveAction;
    private Vector2 _moveInput;
    private InputAction _jumpAction;
    [SerializeField] private float _movementSpeed = 10;
    [SerializeField] private float _jumpHeight = 2;

    private float _turnSmoothVelocity;
    [SerializeField] float _smoothTime = 1;

    private float _gravity;
    [SerializeField] private Vector3 _playerGravity;

    [SerializeField] private Transform _sensorTransform;
    [SerializeField] private float _sensorRadius;
    [SerializeField] private LayerMask _groundLayer;

    void Awake() 
    {
        _characterController = GetComponent<CharacterController>();
        _moveAction = InputSystem.actions["Move"];
        _jumpAction = InputSystem.actions["Jump"];
    }

    void Start()
    {
        _gravity = Physics.gravity.y;
    }

    void Update()
    {
        _moveInput = _moveAction.ReadValue<Vector2>();

        Garvity();

        if(_jumpAction.WasPressedThisFrame() && IsGrounded())
        {
            Jump();
        }

        Movement();
    }

    void Movement()
    {
        Vector3 moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y);

        if(moveDirection != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg; 
            float smoothAngle =  Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _smoothTime);

            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

            _characterController.Move(moveDirection * _movementSpeed * Time.deltaTime);
        }
    }

    void Jump()
    {
        _playerGravity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
    }

    void Garvity()
    {
        if(!IsGrounded())
        {
            _playerGravity.y += _gravity * Time.deltaTime;
        }
        else if(IsGrounded() && _playerGravity.y < 0)
        {
            _playerGravity.y = _gravity;
        }
       
        _characterController.Move(_playerGravity * Time.deltaTime);
    }


    bool IsGrounded()
    {
        return Physics.CheckSphere(_sensorTransform.position, _sensorRadius, _groundLayer);
    }

    void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_sensorTransform.position, _sensorRadius);
    }
}
