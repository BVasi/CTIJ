using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    public int damage = 10;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncreaseDamage(int amount)
    {
        damage += amount;
        Debug.Log("Damage-ul a fost crescut cu " + amount + ". Damage total: " + damage);
    }
}
