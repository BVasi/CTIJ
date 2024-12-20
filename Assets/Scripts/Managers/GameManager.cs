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
    }

    void Start()
    {
        UpdateGameState(GameState.MainMenu);
    }

    public static GameManager Instance { get; private set; }
    private MenuManager _menuManager;
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