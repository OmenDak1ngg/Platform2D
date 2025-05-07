using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class Coin : Item
{
    private CircleCollider2D _collider;

    private void Awake()
    {
        _collider  = GetComponent<CircleCollider2D>();
        _collider.isTrigger = true;
    }
}
