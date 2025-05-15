using System.Collections;
using UnityEngine;

public class EnemyMover : CharactersFlipper
{
    [SerializeField] private float _speed;
    [SerializeField] private float _distanceToRest;
    [SerializeField] private Patrol _patrol;
    [SerializeField] private float _StartRestTime;

    private WaitForSeconds _restTime;
    private float _baseSpeed;
    private Transform _currentTarget;

    protected override void Awake()
    {
        _isFacingRight = false;
        base.Awake();

        _restTime = new WaitForSeconds(_StartRestTime);
        _baseSpeed = _speed;
    }

    protected override void Update()
    {
        base.Update();
    }

    private void FixedUpdate()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        Vector2 direction = _currentTarget.position - transform.position;
        _rigidbody.linearVelocity = direction.normalized * _speed;
    }

    public void SetTarget(Transform target)
    {
        _currentTarget = target;
    }

    public IEnumerator Rest()
    {
        _speed = 0;
        yield return _restTime;
        _speed = _baseSpeed;
    }

    public bool IsReachedTarget()
    {
        return Vector2.SqrMagnitude(transform.position - _currentTarget.position) <= _distanceToRest;
    }
}
