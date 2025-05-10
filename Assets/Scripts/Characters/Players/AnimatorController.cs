using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class AnimatorController : MonoBehaviour
{
    private readonly string MovingParamgeter = "IsMoving";

    private Animator _animator;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _animator = GetComponent<Animator>();   
        _rigidbody = GetComponent<Rigidbody2D>();   
    }

    private void Update()
    {
        bool isMoving = _rigidbody.linearVelocity.x != 0;
        _animator.SetBool(MovingParamgeter, isMoving);
    }
}
