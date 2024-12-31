using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void UpdateGameState(GameState newState)
    {
        if (_state == newState)
        {
            return;
        }

        _state = newState;
        _menuManager.OnGameStateChanged(newState);
    }

    public void AddCoins(int amount)
    {
        _coinManager.AddCoins(amount);
    }

    public void SpendCoins(int amount)
    {
        _coinManager.SpendCoins(amount);
    }

    public bool HasCoins(int amount)
    {
        return _coinManager.HasCoins(amount);
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        _menuManager = GetComponent<MenuManager>();
        _coinManager = GetComponent<CoinManager>();
    }

    void Start()
    {
        UpdateGameState(GameState.MainMenu);
    }

    public static GameManager Instance { get; private set; }
    private MenuManager _menuManager;
    private CoinManager _coinManager;
    private GameState _state;
}

public enum GameState
{
    MainMenu,
    ShopMenu,
    Lose,
    MainGamePlay,
    Quit
}