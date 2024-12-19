using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {

    }

    private PlayerMovement _playerMovement; //to do: refactor to add logic inside controller
    private PlayerStats _playerStats;
}
