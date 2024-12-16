using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    void Start()
    {
        _health = 100;
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

    private void Die()
    {
        Debug.Log("Player has died!");
    }

    private int _health;
    private int _damage;
    private const int NO_HEALTH = 0;
}
