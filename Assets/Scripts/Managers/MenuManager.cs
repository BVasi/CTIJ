using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        isMainGameLoadedForTheFirstTime = true;
    }

    public void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
            {
                HandleMainMenuState();
                break;
            }
            case GameState.ShopMenu:
            {
                HandleShopMenuState();
                break;
            }
            case GameState.Lose:
            {
                HandleLoseState();
                break;
            }
            case GameState.MainGamePlay:
            {
                HandleMainGamePlayState();
                break;
            }
            case GameState.SettingsMenu:
            {
                HandleSettingsMenuState();
                break;
            }
            case GameState.Quit:
            {
                Application.Quit();
                break;
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if ((scene.name.Equals(MAIN_GAME_SCENE_NAME)) && (WaveManager.Instance != null))
        {
            if (!isMainGameLoadedForTheFirstTime)
            {
                WaveManager.Instance.StartNextWave();
                PlayerController.Instance.ResetToSafePosition();
            }
            isMainGameLoadedForTheFirstTime = false;
            return;
        }
        if (WaveManager.Instance == null)
        {
            return;
        }
        WaveManager.Instance.StopWave();
    }

    private void HandleMainMenuState()
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE_NAME);
    }

    private void HandleShopMenuState()
    {
        SceneManager.LoadScene(SHOP_SCENE_NAME);
    }

    private void HandleLoseState()
    {
        SceneManager.LoadScene(GAME_OVER_SCENE_NAME);
        WaveManager.Instance.ResetWaves();
    }

    private void HandleMainGamePlayState()
    {
        SceneManager.LoadScene(MAIN_GAME_SCENE_NAME);
    }

    private void HandleSettingsMenuState()
    {
        SceneManager.LoadScene(SETTINGS_MENU_SCENE_NAME);
    }

    private const string MAIN_MENU_SCENE_NAME = "MainMenu";
    private const string MAIN_GAME_SCENE_NAME = "MainGameScene";
    private const string GAME_OVER_SCENE_NAME = "GameOver";
    private const string SHOP_SCENE_NAME = "Shop";
    private const string SETTINGS_MENU_SCENE_NAME = "SettingsMenu";

    private bool isMainGameLoadedForTheFirstTime; //to do: find better solution than using this variable
}
