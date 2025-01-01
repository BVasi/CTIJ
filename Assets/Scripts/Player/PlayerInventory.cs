using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public void AddItem(ItemType item)
    {
        _items[(int)item]++;
    }

    public void RemoveItem(ItemType item)
    {
        if (!HasItem(item))
        {
            return;
        }
        _items[(int)item]--;
    }

    public bool HasItem(ItemType item)
    {
        return _items[(int)item] > NO_ITEM;
    }

    void Start()
    {
        _items = new int[System.Enum.GetValues(typeof(ItemType)).Length];
    }

    private int[] _items;
    private const int NO_ITEM = 0;
}