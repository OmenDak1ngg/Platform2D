using UnityEngine;

public class PlayerMover : CharactersMover
{
    private static string Horizontal = "Horizontal";

    [SerializeField] private float _jumpForce;
    [SerializeField] private GroundChecker _groundChecker;

    private float _moveHorizontal;
    private bool _isJump;

    protected override void Awake()
    {
        base.Awake();

        _isLookingRight = true;
        _isJump = false;
        _rigidbody = GetComponent<Rigidbody2D>();    
    }

    protected override void Update()
    {
        base.Update();

        ReadUserInput();
    }

    private void FixedUpdate()
    {
        Move();
        
        if(_isJump)
            Jump();
    }

    private void Jump()
    {
        if (_groundChecker.IsGrounded == false)
            return;

        _rigidbody.AddForce(new Vector2(0, _jumpForce),ForceMode2D.Impulse);
        _isJump = false;
    }

    private void ReadUserInput()
    {
        _moveHorizontal = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(KeyCode.Space))
            _isJump = true;
    }

    private void Move()
    {
        _rigidbody.linearVelocity = new Vector2(_moveHorizontal * _speed, _rigidbody.linearVelocity.y);
    }
}
