using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public void AddItem(ItemType item)
    {
        switch (item)
        {
            //ACTIVE
            case ItemType.Health:
            case ItemType.Shield:
            {
                _playerInventory.AddItem(item);
                break;
            }
            //PASIVE
            case ItemType.Speed:
            {
                _playerMovement.IncreaseSpeed(2);
                break;
            }
            case ItemType.Strenght:
            {
                _playerMovement.IncreaseDamage(10);
                break;
            }
        }
    }

    public void ResetToSafePosition()
    {
        transform.position = SAFE_POSITION;
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
        _playerMovement = GetComponent<PlayerMovement>();
        _playerStats = GetComponent<PlayerStats>();
        _playerInventory = GetComponent<PlayerInventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (_playerStats.IsFullHealth())
            {
                return;
            }
            if (!_playerInventory.HasItem(ItemType.Health))
            {
                return;
            }
            _playerStats.Heal(20); //to do: not hardcoded
            _playerInventory.RemoveItem(ItemType.Health);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (_playerStats.IsShielded())
            {
                return;
            }
            if (!_playerInventory.HasItem(ItemType.Shield))
            {
                return;
            }
            _playerStats.Shield();
            _playerInventory.RemoveItem(ItemType.Shield);
        }
    }

    public static PlayerController Instance { get; private set; }
    private PlayerMovement _playerMovement; //to do: refactor to add logic inside controller
    private PlayerStats _playerStats;
    private PlayerInventory _playerInventory;

    private readonly Vector3 SAFE_POSITION = new Vector3(0, -2, 0);
}
