using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRewarder : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(PLAYER_TAG))
        {
            GameManager.Instance.AddCoins(WORTH);
            Destroy(gameObject);
        }
    }

    private const string PLAYER_TAG = "Player";
    private const int WORTH = 1;
}