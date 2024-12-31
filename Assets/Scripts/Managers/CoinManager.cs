using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour //to do: refactor inside playerstats
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
        if (!HasCoins(amount))
        {
            return;
        }
        _coins -= amount;
    }

    public bool HasCoins(int amount)
    {
        return _coins > amount;
    }

    private int _coins;
    private const int STARTING_COINS = 0;
}
