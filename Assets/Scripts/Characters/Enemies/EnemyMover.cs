using UnityEngine;

public class EnemyMover : CharactersMover
{
    [SerializeField] private Transform[] _patrols;
    [SerializeField] private float _startRestTime;
    [SerializeField] private float _distanceToRest;

    private float _restTime;
    private Transform _currentTarget;
    private int _maxPatrolsIndex;
    private int _currentPatrolIndex;
    private float _baseSpeed;

    protected override void Awake()
    {
        base.Awake();

        _baseSpeed = _speed;
        _isLookingRight = false;
        _restTime = _startRestTime;
        _maxPatrolsIndex = _patrols.Length - 1;

        _currentPatrolIndex = 0;
        _currentTarget = _patrols[_currentPatrolIndex];
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

    private void UpdatePatrolMovement()
    {
        if (Vector2.SqrMagnitude(transform.position - _currentTarget.position) <= _distanceToRest)
        {
            if (_restTime <= 0)
            {                
                if (_currentPatrolIndex == _maxPatrolsIndex)
                    _currentPatrolIndex = 0;
                else
                    _currentPatrolIndex = _currentPatrolIndex + 1;

                _currentTarget = _patrols[_currentPatrolIndex];
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

    private void MoveToTarget()
    {
        Vector2 direction = _currentTarget.position - transform.position;
        _rigidbody.linearVelocity = direction.normalized * _speed;
    }
}
