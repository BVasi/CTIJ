using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour //to do: refactor inside playerstats
{
    
    void Awake()
    {
        _coins = STARTING_COINS;
        UpdateCoinCounterUI();
    }

    public void AddCoins(int amount)
    {
        _coins += amount;
        UpdateCoinCounterUI();
    }

    public void SpendCoins(int amount)
    {
        if (!HasCoins(amount))
        {
            return;
        }
        _coins -= amount;
        UpdateCoinCounterUI();
    }

    public bool HasCoins(int amount)
    {
        return _coins >= amount;
    }

    private void UpdateCoinCounterUI()
    {
        if (coinCounterText != null)
        {
            coinCounterText.text = "Coins: " + _coins;
        }
    }

    private int _coins;
    private const int STARTING_COINS = 0;
    [SerializeField] private Text coinCounterText;
}
