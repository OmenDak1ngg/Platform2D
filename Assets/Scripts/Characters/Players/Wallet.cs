using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class Wallet : MonoBehaviour
{
    private int _coinsCollected;

    private void Awake()
    {
        _coinsCollected = 0;
    }

    public void AddCoin()
    {
        _coinsCollected++;
        Debug.Log(_coinsCollected);
    }
}
