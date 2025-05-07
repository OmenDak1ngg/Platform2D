using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{
    private static int AttackTrigger = Animator.StringToHash("Attack");
    private static string MovingParamgeter = "IsMoving";

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

    public void StartAttackAnimation()
    {
        _animator.SetTrigger(AttackTrigger);
    }
}
