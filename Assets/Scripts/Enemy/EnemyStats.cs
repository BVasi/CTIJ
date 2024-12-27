using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    void Start()
    {
        _health = 30;
        _damage = 10;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health < NO_HEALTH)
        {
            Die();
        }
    }

    public void IncreaseDamage(int valueToIncrease)
    {
        _damage += valueToIncrease;
    }

    public void DecreaseDamage(int valueToDecrease)
    {
        _damage -= valueToDecrease;
    }

    public void IncreaseHealth(int valueToIncrease)
    {
        _health += valueToIncrease;
    }

    public void DecreaseHealth(int valueToDecrease)
    {
        _health -= valueToDecrease;
    }

    private void Die()
    {
        DropCoins(Random.Range(MIN_COINS_TO_DROP, MAX_COINS_TO_DROP));
        Destroy(gameObject);
    }

    private void DropCoins(int amountToDrop)
    {
        for (int coinIndex = FIRST_COIN_INDEX; coinIndex < amountToDrop; coinIndex++)
        {
            Vector3 dropPosition = transform.position + new Vector3(Random.Range(-1f, 1f), 0f, 0f);
            dropPosition.y = -2.67f;
            Instantiate(_coinPrefab, dropPosition, Quaternion.identity);
        }
    }

    [SerializeField] private GameObject _coinPrefab;
    private int _health;
    private int _damage;
    private const int NO_HEALTH = 0;
    private const int MIN_COINS_TO_DROP = 0;
    private const int MAX_COINS_TO_DROP = 5;
    private const int FIRST_COIN_INDEX = 0;
}
