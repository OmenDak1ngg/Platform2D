using UnityEngine;

public class CoinSpawnpoint : MonoBehaviour
{
    public bool IsUsed { get; private set; }

    private void Start()
    {
        IsUsed = false;
    }

    private void SetUsed()
    {
        IsUsed = true;
    }

    private void UnsetUsed()
    {
        IsUsed = false;
    }
}
