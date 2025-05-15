using UnityEngine;

public class PlayerMover : CharactersFlipper
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private GroundChecker _groundChecker;

    protected override void Awake()
    {
        _isFacingRight = true;

        base.Awake();

        _rigidbody = GetComponent<Rigidbody2D>();    
    }

    protected override void Update()
    {
        base.Update();
    }

    public void Jump()
    {
        if (_groundChecker.IsGrounded == false)
            return;

        _rigidbody.AddForce(new Vector2(0, _jumpForce),ForceMode2D.Impulse);
    }

    public void Move(float direction)
    {
        _rigidbody.linearVelocity = new Vector2(direction * _speed, _rigidbody.linearVelocity.y);
    }
}
