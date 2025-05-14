using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;
using System.Collections;

public class CoinSpawner : MonoBehaviour
{
    private readonly string _spawnpointMask = "SpawnPoint";
    private readonly string _coinMask = "Coin";

    [SerializeField] private Coin _prefab;
    [SerializeField] private int _capacity;
    [SerializeField] private CoinSpawnpoint[] _spawnpoints;
    [SerializeField] private float _spawnRate;
    [SerializeField] private float _coinCheckRaidus;

    private ObjectPool<Coin> _pool;
    private CoinSpawnpoint _currentSpawnpoint;
    private List<CoinSpawnpoint> _avalaibleSpawnPoints;

    private void Awake()
    {
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
            _currentSpawnpoint = GetRandomAvalaibleSpawnPoint();

            if (_currentSpawnpoint != null)
            {
                _pool.Get();
            }

            yield return delay;
        }
    }

    private void CreatePool()
    {
        _pool = new ObjectPool<Coin>(
            createFunc: () => InstantiateCoin(),
            actionOnGet: (coin) => SpawnCoin(coin),
            actionOnRelease: (coin) => coin.gameObject.SetActive(false),
            actionOnDestroy: (coin) => DestoyCoin(coin),
            defaultCapacity: _capacity
            );
    }
    
    private Coin InstantiateCoin()
    {
        Coin newCoin = Instantiate(_prefab);
        newCoin.Taked += ReleaseCoin;

        return newCoin;
    }

    private void SpawnCoin(Coin coin)
    {
        coin.gameObject.transform.position = _currentSpawnpoint.transform.position;
        coin.gameObject.SetActive(true);
    }

    private void DestoyCoin(Coin coin)
    {
        coin.Taked -= ReleaseCoin;  
        Destroy( coin.gameObject );
    }

    private void ReleaseCoin(Coin coin)
    {
        _pool.Release(coin);
        coin.gameObject.SetActive(false);
        ReturnSpawnPointToAvalaibles(coin);
    }

    private void ReturnSpawnPointToAvalaibles(Coin coin)
    {
        Collider2D spawnpoint = Physics2D.OverlapCircle(coin.transform.position, _coinCheckRaidus, LayerMask.GetMask(_spawnpointMask));
        _avalaibleSpawnPoints.Add(spawnpoint.GetComponent<CoinSpawnpoint>());
    }

    private CoinSpawnpoint GetRandomAvalaibleSpawnPoint()
    {
        _avalaibleSpawnPoints = new List<CoinSpawnpoint>(_spawnpoints);
        int RandomSpawnPointIndex;

        while(_avalaibleSpawnPoints.Count > 0)
        {
            RandomSpawnPointIndex = Random.Range(0, _avalaibleSpawnPoints.Count - 1);
            CoinSpawnpoint spawnpoint = _avalaibleSpawnPoints[RandomSpawnPointIndex];
            Collider2D coin = Physics2D.OverlapCircle(spawnpoint.transform.position, _coinCheckRaidus, LayerMask.GetMask(_coinMask));

            if (coin != null)
            {
                _avalaibleSpawnPoints.Remove(spawnpoint);
            }
            else
            {
                return spawnpoint;
            }
        }

        return null;
    }
}
