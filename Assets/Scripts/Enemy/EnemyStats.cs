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
        Destroy(gameObject);
    }

    private int _health;
    private int _damage;
    private const int NO_HEALTH = 0;
}
