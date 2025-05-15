using UnityEditor.Compilation;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharactersFlipper : Character
{
    private const float _threshold = 0.01f;

    private Quaternion _baseRotation;
    private Quaternion _flippedRotation;
    private float _rotationAngle = 180;

    protected bool _isFacingRight;
    protected Rigidbody2D _rigidbody;

    protected virtual void Awake()
    {
        Vector2 baseEuler = transform.eulerAngles;
        _baseRotation = transform.rotation;
        _flippedRotation = Quaternion.Euler(baseEuler.x, baseEuler.y + _rotationAngle, 0f);
        _rigidbody = GetComponent<Rigidbody2D>();

        if(_isFacingRight == false)
        {
            transform.rotation = _flippedRotation;
            _isFacingRight = true;
        }
    }

    protected virtual void Update()
    {
        UpdateFacingDirection();
    }

    private void Flip()
    {
        Quaternion rotation = _isFacingRight ? _baseRotation : _flippedRotation;
        _isFacingRight = !_isFacingRight;
        transform.rotation = rotation;
    }
   
    private void UpdateFacingDirection()
    {
        if (_rigidbody.linearVelocityX > _threshold)
        {
            if(_isFacingRight == false)
                Flip();          
        }

        if(_rigidbody.linearVelocityX < -_threshold)
        {
            if (_isFacingRight)
                Flip();
        }
    }
}
