using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;
using System.Collections;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _prefab;
    [SerializeField] private int _capacity;
    [SerializeField] private CoinSpawnpoint[] _spawnpoints;
    [SerializeField] private float _spawnRate;
    [SerializeField] private Collector _collector;

    private ObjectPool<Coin> _pool;
    private CoinSpawnpoint _currentSpawnpoint;
    private float _coinCheckRaidus;

    private void OnEnable()
    {
        _collector.TakedCoin += ReleaseCoin;    
    }

    private void OnDisable()
    {
        _collector.TakedCoin -= ReleaseCoin;
    }

    private void Awake()
    {
        _coinCheckRaidus = 1.5f;
        CreatePool();
    }

    private void Start()
    {
        StartSpawning();
    }

    private void StartSpawning()
    {
        StartCoroutine(SpawnWithDelay());
    }

    private IEnumerator SpawnWithDelay()
    {
        WaitForSeconds delay = new WaitForSeconds(_spawnRate);

        while (true)
        {
            _currentSpawnpoint = GetRandomSpawnPoint();

            if (_currentSpawnpoint != null)
            {
                Debug.Log("спавн монеты");
                _pool.Get();
                yield return delay;
            }
            else
            {
                Debug.Log("null");
            }
        }
    }

    private void CreatePool()
    {
        _pool = new ObjectPool<Coin>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (coin) => SpawnCoin(coin),
            actionOnRelease: (coin) => ReleaseCoin(coin),
            actionOnDestroy: (coin) => Destroy(coin.gameObject),
            defaultCapacity: _capacity
            );
    }

    private void ReleaseCoin(Coin coin)
    {
        coin.gameObject.SetActive(false);
        _pool.Release(coin);
    }

    private void SpawnCoin(Coin coin)
    {
        coin.gameObject.transform.position = _currentSpawnpoint.transform.position;
        coin.gameObject.SetActive(true);
    }

    private CoinSpawnpoint GetRandomSpawnPoint()
    {
        List<CoinSpawnpoint> avalaibleSpawnPoints = new List<CoinSpawnpoint>(_spawnpoints);
        int RandomSpawnPointIndex;

        while(avalaibleSpawnPoints.Count > 0)
        {
            RandomSpawnPointIndex = Random.Range(0, avalaibleSpawnPoints.Count - 1);
            CoinSpawnpoint spawnpoint = avalaibleSpawnPoints[RandomSpawnPointIndex];
            Collider2D coin = Physics2D.OverlapCircle(spawnpoint.transform.position, _coinCheckRaidus, LayerMask.GetMask("Coin"));

            if (coin != null)
            {
                avalaibleSpawnPoints.Remove(spawnpoint);
            }
            else
            {
                return spawnpoint;
            }
        }

        Debug.Log("null");
        return null;
    }
}
