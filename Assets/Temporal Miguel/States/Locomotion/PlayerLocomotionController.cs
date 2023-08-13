using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotionController : MonoBehaviour
{
    float _verticalInputValue;
    float _horizontalInputValue;

    [SerializeField] float _speed;
    [SerializeField] float _newSpeed;
    float _currentSpeed;

    [SerializeField] float _jumpForce;
    [SerializeField] float _walkSpeed;
    [SerializeField] float _runSpeed;
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _changeSpeedTime;

    Rigidbody _rigid;

    Vector3 _currentRotation;

    Animator _animatorController;

    [SerializeField] LayerMask _groundLayers;
    [SerializeField] Transform _groundDetectionCenter;
    [SerializeField] float _groundDetectionRadius;

    bool _grounded;
    bool _jumpActivated;

    PerspectiveEnum currentPerspective;

    PlayerIdleState _idleState = new PlayerIdleState();
    PlayerWalkState _walkState = new PlayerWalkState();
    PlayerRunState _runState = new PlayerRunState();

    PlayerLocomotionBaseState _movementState;

    public float CurrentSpeed { get => _currentSpeed; set => _currentSpeed = value; }
    public PlayerIdleState IdleState { get => _idleState; set => _idleState = value; }
    public PlayerWalkState WalkState { get => _walkState; set => _walkState = value; }
    public PlayerRunState RunState { get => _runState; set => _runState = value; }
    public float WalkSpeed { get => _walkSpeed; set => _walkSpeed = value; }
    public float RunSpeed { get => _runSpeed; set => _runSpeed = value; }
    public float NewSpeed { get => _newSpeed; set => _newSpeed = value; }
    public float VerticalInputValue { get => _verticalInputValue; set => _verticalInputValue = value; }
    public float HorizontalInputValue { get => _horizontalInputValue; set => _horizontalInputValue = value; }

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _animatorController = GetComponent<Animator>();

        _movementState = _idleState;
    }

    // Update is called once per frame
    void Update()
    {
        _verticalInputValue = currentPerspective == PerspectiveEnum.Side ? 0 : Input.GetAxis("Vertical");

        _horizontalInputValue = Input.GetAxis("Horizontal");

        _speed = Mathf.MoveTowards(_speed, _newSpeed, _changeSpeedTime * Time.deltaTime);
        _currentSpeed = Mathf.Abs(_verticalInputValue != 0 ? _verticalInputValue : _horizontalInputValue) * _speed;

        _animatorController.SetFloat("Speed", _currentSpeed);

        ChangeRotation();

        _movementState.UpdateState(this);

        //if (_verticalInputValue != 0 || _horizontalInputValue != 0)
        //    _rigid.AddForce(transform.forward * (Mathf.Abs(_verticalInputValue != 0 ? _verticalInputValue : _horizontalInputValue) * _speed), ForceMode.Acceleration);



        //if (Input.GetKeyDown(KeyCode.O) && !_grabbedObject)
        //    _animatorController.SetTrigger("Attack");


    }

    private void FixedUpdate()
    {
        Collider[] _groundDetections = Physics.OverlapSphere(_groundDetectionCenter.position, _groundDetectionRadius, _groundLayers);
        _grounded = _groundDetections.Length > 0 ? true : false;

        _animatorController.SetBool("Grounded", _grounded);

        if (_jumpActivated)
        {
            Debug.Log("Salto?");
            _animatorController.SetTrigger("Jump");
            _rigid.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _jumpActivated = false;
        }

        _movementState.FixedState(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(_groundDetectionCenter.position, _groundDetectionRadius);
    }

    public void ChangeRotation()
    {
        if (_horizontalInputValue != 0 && _verticalInputValue != 0)
        {
            if (_verticalInputValue < 0)
                _currentRotation = new Vector3(_currentRotation.x, _horizontalInputValue < 0 ? 45 : -45, _currentRotation.z);

            else
                _currentRotation = new Vector3(_currentRotation.x, _horizontalInputValue < 0 ? 135 : -135, _currentRotation.z);
        }

        else
        {
            if (_horizontalInputValue != 0)
                _currentRotation = new Vector3(_currentRotation.x, _horizontalInputValue < 0 ? 90 : -90, _currentRotation.z);

            else if (_verticalInputValue != 0)
                _currentRotation = new Vector3(_currentRotation.x, _verticalInputValue < 0 ? 0 : -180, _currentRotation.z);
        }

        if (currentPerspective == PerspectiveEnum.Top)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(_currentRotation), _rotationSpeed * Time.deltaTime);
        else
            transform.rotation = Quaternion.Euler(_currentRotation);
    }

    public void Movement()
    {
        if (_verticalInputValue != 0 || _horizontalInputValue != 0)
            _rigid.velocity = transform.forward * _speed;
    }

    public void Jump()
    {
        if (_grounded)
            _jumpActivated = true;
    }

    public void ChangeState(PlayerLocomotionBaseState _newState)
    {
        if (_movementState != null)
            _movementState.ExitState(this);

        _movementState = _newState;
        _movementState.StartState(this);
    }

    public void ChangePerspective(PerspectiveEnum newPerspective)
    {
        currentPerspective = newPerspective;
    }
    private void OnEnable()
    {
        GameDelegateHelper.changePerspective += ChangePerspective;
    }

    private void OnDisable()
    {
        GameDelegateHelper.changePerspective -= ChangePerspective;
    }

}