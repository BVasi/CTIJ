using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour //to do: refactor inside playerstats
{
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        _coins = STARTING_COINS;
    }

    public void AddCoins(int amount)
    {
        _coins += amount;
        UpdateUiCoinCounter();
    }

    public void SpendCoins(int amount)
    {
        if (!HasCoins(amount))
        {
            return;
        }
        _coins -= amount;
        UpdateUiCoinCounter();
    }

    public bool HasCoins(int amount)
    {
        return _coins >= amount;
    }

    private void UpdateUiCoinCounter()
    {
        if (!_coinCounterText)
        {
            return;
        }
        _coinCounterText.text = $"{_coins} coins";
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!scene.name.Equals(MAIN_GAME_SCENE_NAME))
        {
            return;
        }
        _coinCounterText = GameObject.FindWithTag(COINS_COUNTER_UI_TAG).GetComponent<TextMeshProUGUI>();
        UpdateUiCoinCounter();
    }

    private int _coins;
    private TextMeshProUGUI _coinCounterText;
    private const int STARTING_COINS = 0;
    private const string MAIN_GAME_SCENE_NAME = "MainGameScene";
    private const string COINS_COUNTER_UI_TAG = "CoinsCounter";
}
