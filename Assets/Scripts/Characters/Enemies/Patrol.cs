using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private Transform[] _points;

    private int _currentPatrolIndex;
    private int _maxPatrolsIndex;

    private void Awake()
    {
        _currentPatrolIndex = 0;
        _maxPatrolsIndex = _points.Length-1;
    }

    public Transform GetNextPatrolPoint()
    {
        if (_currentPatrolIndex == _maxPatrolsIndex)
            _currentPatrolIndex = 0;
        else
            _currentPatrolIndex = _currentPatrolIndex + 1;

        return _points[_currentPatrolIndex];
    }

    public Transform GetFirstPatrolPoint()
    {
        return _points[0];
    }
}
