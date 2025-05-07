using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float _startCooldown;
    [SerializeField] private Transform _attackPosition;
    [SerializeField] private LayerMask _enemyLayermask;
    [SerializeField] private float _radiusAttack;
    [SerializeField] private int _damage;
    [SerializeField] private AnimatorController _animatorController;

    private float _cooldown;
    private Collider2D[] _attackedEnemies;

    private void Awake()
    {
        _cooldown = 0;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F) && _cooldown <= 0)
        {
            _animatorController.StartAttackAnimation();
            _cooldown = _startCooldown;
        }
        else
        {
            _cooldown -= Time.deltaTime;
        }
    }

    private void OnAttack()
    {
        _attackedEnemies = Physics2D.OverlapCircleAll(_attackPosition.position, _radiusAttack, _enemyLayermask);

        foreach(var enemy in _attackedEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(_damage);
        }

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_attackPosition.position,_radiusAttack);
    }
}
