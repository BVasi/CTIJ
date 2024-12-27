using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    void Awake()
    {
        _coins = STARTING_COINS;
    }

    public void AddCoins(int amount)
    {
        _coins += amount;
    }

    public void SpendCoins(int amount)
    {
        if (_coins < amount)
        {
            return;
        }
        _coins -= amount;
    }

    private int _coins;
    private const int STARTING_COINS = 0;
}
