using System;
using UnityEngine;

[RequireComponent(typeof(Wallet))]
public class Collector : MonoBehaviour
{
    private Wallet _wallet;

    public event Action<Coin> TakedCoin;

    private void Awake()
    {
        _wallet = transform.GetComponent<Wallet>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.transform.TryGetComponent(out Coin coin))
        {
            _wallet.AddCoin();
            TakedCoin?.Invoke(coin);
        }
    }
}
