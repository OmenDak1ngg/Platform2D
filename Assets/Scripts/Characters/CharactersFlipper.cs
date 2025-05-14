using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharactersFlipper : Character
{
    private const float _threshold = 0.01f;

    protected bool _isLookingRight;
    protected Rigidbody2D _rigidbody;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        UpdateFacingDirection();
    }

    private void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        rotation.y += 180;
        transform.rotation = Quaternion.Euler(rotation);
    }
   
    private void UpdateFacingDirection()
    {
        if (_rigidbody.linearVelocityX > _threshold)
        {
            if (_isLookingRight == false)
            {
                Flip();
                _isLookingRight = true;
            }
        }

        if (_rigidbody.linearVelocityX < -_threshold)
        {
            if (_isLookingRight == true)
            {
                Flip();
                _isLookingRight = false;
            }
        }
    }
}
