using UnityEngine;

public class EnemyMover : CharactersMover
{
    [SerializeField] private float _startRestTime;
    [SerializeField] private float _distanceToRest;
    [SerializeField] private Patrol _patrol;

    private float _restTime;
    private float _baseSpeed;

    private Transform _currentTarget;

    protected override void Awake()
    {
        base.Awake();

        _baseSpeed = _speed;
        _isLookingRight = false;
        _restTime = _startRestTime;
    }

    protected override void Update()
    {
        base.Update();

        UpdatePatrolMovement();
    }

    private void FixedUpdate()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        Debug.Log($"{_currentTarget.position} - таргрет\n{transform.position} - враг");
        Vector2 direction = _currentTarget.position - transform.position;
        _rigidbody.linearVelocity = direction.normalized * _speed;
    }

    private void UpdatePatrolMovement()
    {
        if (Vector2.SqrMagnitude(transform.position - _currentTarget.position) <= _distanceToRest)
        {
            if (_restTime <= 0)
            {
                _currentTarget = _patrol.GetNextPatrolPoint();
                _restTime = _startRestTime;

                _speed = _baseSpeed;
            }
            else
            {
                _speed = 0;

                _restTime -= Time.deltaTime;
            }
        }
    }
}
