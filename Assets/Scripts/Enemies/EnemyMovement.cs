
using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform[] _patrols;
    [SerializeField] private float _speed;
    [SerializeField] private float _startRestTime;
    [SerializeField] private float _distanceToRest;
    [SerializeField] private float _visionDistance;
    [SerializeField] private float _movementThreshold = 0.01f;

    private Transform _currentTarget;
    private float _restTime;
    private bool _isFacingLeft;

    private bool _isChasingPlayer;
    private Vector2 _rayDirection;
    private int _maxPatrolsIndex;
    private int _currentPatrolIndex;

    private void Awake()
    {
        _isFacingLeft = true;
        _restTime = _startRestTime;
        _maxPatrolsIndex = _patrols.Length - 1;

        _currentPatrolIndex = 0;
        _currentTarget = _patrols[_currentPatrolIndex];
    }

    private void Update()
    {
        OnPlayerDetected();
        UpdateFacingDirection();
        UpdatePatrolMovement();
        MoveToTarget();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position,_rayDirection * _visionDistance);
    }

    private void UpdatePatrolMovement()
    {
        if (_isChasingPlayer)
            return;

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
            }
            else
            {
                _restTime -= Time.deltaTime;
            }
        }
    }

    private void OnPlayerDetected()
    {
        _rayDirection = _isFacingLeft ? Vector2.left : Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _rayDirection, _visionDistance, LayerMask.GetMask("Player"));

        if (hit.collider != null)
        {
            _isChasingPlayer = true;
            _currentTarget = hit.collider.transform;
        }
        else if (_isChasingPlayer)
        {
            StartCoroutine(LostPlayer());
        }
    }

    private IEnumerator LostPlayer()
    {
        float waitPlayer = 3f;

        yield return new WaitForSeconds(waitPlayer);
        _currentTarget = FindClosestPatrol();
        _isChasingPlayer = false;
    }

    private Transform FindClosestPatrol()
    {
        float closestDistance = 999;
        float distance;
        Transform closestPatrol = null;

        foreach(var target in _patrols)
        {
             distance = Vector2.SqrMagnitude(target.position - transform.position);

            if(distance < closestDistance)
            {
                closestDistance = distance;
                closestPatrol = target;
            }
        }

        return closestPatrol;
    }

    private void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, _currentTarget.position, _speed * Time.deltaTime);
    }

    private void UpdateFacingDirection()
    {
        float directionToTarget = _currentTarget.position.x - transform.position.x;

        if (directionToTarget < -_movementThreshold)
        {
            if (_isFacingLeft == false)
            {
                Flip();
                _isFacingLeft = true;
            }
        }

        if (directionToTarget > _movementThreshold)
        {
            if (_isFacingLeft)
            {
                Flip();
                _isFacingLeft = false;
            }
        }
        
    }

    private void Flip()
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
