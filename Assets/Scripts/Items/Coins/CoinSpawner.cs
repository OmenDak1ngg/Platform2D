using UnityEngine;
using UnityEngine.Pool;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _prefab;
    [SerializeField] private Enemy[] _enemies;
    [SerializeField] private int _capacity;

    private ObjectPool<Coin> _pool;
    private Vector2 _enemieDeadPosition;

    private void OnEnable()
    {
        foreach(Enemy enemy in _enemies)
        {
            enemy.Dead += EnemieDeath;
        }
    }

    private void OnDisable()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.Dead -= EnemieDeath;
        }
    }

    private void Awake()
    {
        CreatePool();
    }

    private void EnemieDeath(Vector2 position)
    {
        _enemieDeadPosition = position;
        _pool.Get();
    }

    private void CreatePool()
    {
        _pool = new ObjectPool<Coin>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (coin) => DropCoin(coin),
            actionOnRelease: (coin) => coin.gameObject.SetActive(false),
            actionOnDestroy: (coin) => Destroy(coin.gameObject),
            defaultCapacity: _capacity
            );
    }

    private void DropCoin(Coin coin)
    {
        coin.gameObject.SetActive(true);
        coin.transform.position = _enemieDeadPosition;
    }
}
