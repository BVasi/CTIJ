using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
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
            case GameState.Quit:
            {
                Application.Quit();
                break;
            }
        }
    }

    private void HandleMainMenuState()
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE_NAME);
    }

    private void HandleShopMenuState()
    {
        //to do: add shop logic
    }

    private void HandleLoseState()
    {
        SceneManager.LoadScene(GAME_OVER_SCENE_NAME);
    }

    private void HandleMainGamePlayState()
    {
        SceneManager.LoadScene(MAIN_GAME_SCENE_NAME);
    }

    private const string MAIN_MENU_SCENE_NAME = "MainMenu";
    private const string MAIN_GAME_SCENE_NAME = "MainGameScene";
    private const string GAME_OVER_SCENE_NAME = "GameOver";
}
