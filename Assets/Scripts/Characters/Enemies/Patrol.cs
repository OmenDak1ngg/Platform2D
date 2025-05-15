using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private EnemyMover _enemyMover;

    private int _currentPatrolIndex;

    private void Awake()
    {
        _currentPatrolIndex = 0;
        _enemyMover.SetTarget(_points[_currentPatrolIndex]);
    }

    private void Update()
    {
        if (_enemyMover.IsReachedTarget())
        {
            _enemyMover.Rest();
            _enemyMover.SetTarget(GetNextPatrolPoint());
        }
    }

    public Transform GetNextPatrolPoint()
    {
        _currentPatrolIndex = ++_currentPatrolIndex %_points.Length;

        return _points[_currentPatrolIndex];
    }
}
