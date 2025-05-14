using UnityEngine;

public class PlayerMover : CharactersFlipper
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float _jumpForce;
    [SerializeField] private GroundChecker _groundChecker;

    protected override void Awake()
    {
        base.Awake();

        _isLookingRight = true;
        _rigidbody = GetComponent<Rigidbody2D>();    
    }

    protected override void Update()
    {
        base.Update();
    }

    private void FixedUpdate()
    {
        Move();

        if (_inputReader.TryedToJump)
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (_groundChecker.IsGrounded == false)
            return;

        _rigidbody.AddForce(new Vector2(0, _jumpForce),ForceMode2D.Impulse);
        _inputReader.ResetJumpState();
    }

    private void Move()
    {
        _rigidbody.linearVelocity = new Vector2(_inputReader.Direction * _speed, _rigidbody.linearVelocity.y);
    }
}
