using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(GroundDetectionHandler))]

public class Mover : MonoBehaviour, IFlippable
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody;
    private PlayerInput _playerInput;
    private GroundDetectionHandler _groundDetectionHandler;

    private float _jumpCount = 1;
    private float _maxJumpCount = 1;
    private bool _isFastFallKeyPressed;

    public event Action<bool> Running;
    public event Action Jumped;

    public Vector3 HorizontalDirection { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
        _groundDetectionHandler = GetComponent<GroundDetectionHandler>();
    }

    private void OnEnable()
    {      
        _playerInput.SpacePressed += Jump;
        _playerInput.OnHorizontalDirectionInput += GetHorizontalDirection;
        _playerInput.IsKeyPressed += ChangeGravityScale;
    }

    private void OnDisable()
    {
        _playerInput.SpacePressed -= Jump;
        _playerInput.OnHorizontalDirectionInput -= GetHorizontalDirection;
        _playerInput.IsKeyPressed -= ChangeGravityScale;
    }

    private void FixedUpdate()
    {
        transform.Translate(_speed * Time.deltaTime * HorizontalDirection);

        if (_groundDetectionHandler.IsGround == true)
        {
            _jumpCount = _maxJumpCount;
        }

        if (HorizontalDirection.x != 0)
        {
            Running?.Invoke(true);
        }
        else
        {
            Running?.Invoke(false);
        }
    }

    private void Jump()
    {
        if (_jumpCount == _maxJumpCount && _groundDetectionHandler.IsGround)
        {
            AddForce();
            Jumped?.Invoke();
            _jumpCount--;
        }
        else if (_jumpCount > 0 && _groundDetectionHandler.IsGround == false)
        {
            AddForce();
            Jumped?.Invoke();
            _jumpCount--;
        }
    }

    private void AddForce()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0f);
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void GetHorizontalDirection(Vector3 direction)
    {
        HorizontalDirection = direction;
    }

    private void ChangeGravityScale(bool isKeyPressed)
    {
        if (isKeyPressed)
        {
            _rigidbody.gravityScale = 3;
        }
        else
        {
            _rigidbody.gravityScale = 1;
        }
    }
}