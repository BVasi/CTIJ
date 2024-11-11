using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Shop Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string description;
    public bool isPurchased = false;
    public int damageBoost = 10;
}
