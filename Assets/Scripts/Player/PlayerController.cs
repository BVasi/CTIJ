using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public void ImproveStat(StatType stat, int improveAmount)
    {
        switch (stat)
        {
            case StatType.Health:
            {
                _playerStats.IncreaseMaxHealth(improveAmount);
                break;
            }
            case StatType.Speed:
            {
                _playerMovement.IncreaseSpeed(improveAmount);
                break;
            }
            case StatType.Damage:
            {
                _playerMovement.IncreaseDamage(improveAmount);
                break;
            }
        }
    }

    public void ReduceStat(StatType stat, int reduceAmount)
    {
        switch (stat)
        {
            case StatType.Health:
            {
                _playerStats.DecreaseMaxHealth(reduceAmount);
                break;
            }
            case StatType.Speed:
            {
                _playerMovement.DecreaseSpeed(reduceAmount);
                break;
            }
            case StatType.Damage:
            {
                _playerMovement.DecreaseDamage(reduceAmount);
                break;
            }
        }
    }

    public void AddItem(ItemType item)
    {
        switch (item)
        {
            case ItemType.Health:
            case ItemType.TemporaryDamage:
            case ItemType.TemporarySpeed:
            case ItemType.Shield:
            {
                _playerInventory.AddItem(item);
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
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (!_playerInventory.HasItem(ItemType.TemporaryDamage))
            {
                return;
            }
            StartCoroutine(ApplyTemporaryBoost(ItemType.TemporaryDamage, 20, 8)); //to do: not hardcoded
            _playerInventory.RemoveItem(ItemType.TemporaryDamage);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (!_playerInventory.HasItem(ItemType.TemporarySpeed))
            {
                return;
            }
            StartCoroutine(ApplyTemporaryBoost(ItemType.TemporarySpeed, 7, 10)); //to do: not hardcoded
            _playerInventory.RemoveItem(ItemType.TemporarySpeed);
        }
    }

    private IEnumerator ApplyTemporaryBoost(ItemType itemType, int boostAmount, float duration)
    {
        ImproveStat((StatType)(int)itemType, boostAmount);
        yield return new WaitForSeconds(duration);
        ReduceStat((StatType)(int)itemType, boostAmount);
    }

    public static PlayerController Instance { get; private set; }
    private PlayerMovement _playerMovement; //to do: refactor to add logic inside controller
    private PlayerStats _playerStats;
    private PlayerInventory _playerInventory;

    private readonly Vector3 SAFE_POSITION = new Vector3(0, -2, 0);
    private const string ITEM_TAG = "Item";
}
