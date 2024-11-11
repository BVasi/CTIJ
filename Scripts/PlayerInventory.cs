using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;
    public List<Item> itemsInInventory = new List<Item>();

    private int maxItems = 2;

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

    // Adaugă un item în inventar dacă există spațiu
    public void AddItemToInventory(Item item)
    {
        if (itemsInInventory.Count < maxItems)
        {
            itemsInInventory.Add(item);
            Debug.Log(item.itemName + " a fost adăugat în inventar.");
        }
        else
        {
            Debug.Log("Inventarul este plin!");
        }
    }
}
