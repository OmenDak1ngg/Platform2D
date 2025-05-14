using System;
using UnityEngine;

public class Coin : Item
{
    public event Action<Coin> Taked;

    private void OnDisable()
    {
        Taked?.Invoke(this);
    }
}
