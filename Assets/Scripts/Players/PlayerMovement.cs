using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private static string Horizontal = "Horizontal";

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _groundCheckDistance = 1f;

    private RaycastHit2D _hit;
    private float _moveHorizontal;
    private Rigidbody2D _rigidody;
    private bool _isLookingRight;

    private void Awake()
    {
        _isLookingRight = true;
        _rigidody = GetComponent<Rigidbody2D>();    
    }

    private void Update()
    {
        Debug.DrawRay(_rigidody.position , Vector2.down * _groundCheckDistance, Color.red);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move();
        UpdateFacingDirection();
    }

    private void Move()
    {
        _moveHorizontal = Input.GetAxis(Horizontal);
        
        _rigidody.linearVelocity = new Vector2(_moveHorizontal * _speed, _rigidody.linearVelocity.y);
    }

    private void UpdateFacingDirection()
    {
        if(_rigidody.linearVelocityX > 0)
        {
            if(_isLookingRight == false)
            {
                Flip();
                _isLookingRight = true;
            }
        }

        if(_rigidody.linearVelocityX < 0)
        {
            if(_isLookingRight == true)
            {
                Flip();
                _isLookingRight = false;
            }
        }
    }

    private void Flip()
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;   
    }

    private void Jump()
    {
        if (IsGrounded() == false)
            return;

        _rigidody.AddForce(new Vector2(0, _jumpForce),ForceMode2D.Impulse);
    }

    private bool IsGrounded()
    {
        _hit = Physics2D.Raycast(_rigidody.position, Vector2.down, _groundCheckDistance, LayerMask.GetMask("Ground"));
        return _hit.collider != null;
    }
}
