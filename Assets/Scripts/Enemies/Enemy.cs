using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;


    public event Action<Vector2> Dead;

    public void TakeDamage(int damage)
    {
        if(_health >= damage)
        {
            _health -= damage;
        }
        else
        {
            _health = 0;
            Dead?.Invoke(transform.position);
            Destroy(gameObject);
        }
    }
}
