using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    void Start()
    {
        foreach (ButtonGameStatePair buttonGameStatePair in _buttons)
        {
            buttonGameStatePair._button.onClick.AddListener(() => OnButtonClicked(buttonGameStatePair));
        }
    }

    private void OnButtonClicked(ButtonGameStatePair clickedButton)
    {
        GameManager.Instance.UpdateGameState(clickedButton._state);
    }

    [SerializeField] private ButtonGameStatePair[] _buttons;
}

[System.Serializable]
public class ButtonGameStatePair
{
    public Button _button;
    public GameState _state;
}