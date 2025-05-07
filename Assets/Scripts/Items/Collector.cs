using UnityEngine;

[RequireComponent(typeof(Player))]
public class Collector : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = transform.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.transform.TryGetComponent<Coin>(out Coin coin))
        {
            _player.AddCoin();
            Destroy(coin.gameObject);
        }
    }
}
