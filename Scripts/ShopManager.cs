using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public List<Item> availableItems;
    public GameObject shopUI;
    public Dictionary<Item, Button> itemButtons = new Dictionary<Item, Button>();
    private List<Item> randomItems = new List<Item>();

    // Start is called before the first frame update
    void Start()
    {
        shopUI.SetActive(false);
        foreach (var item in availableItems)
        {
            Button button = GameObject.Find(item.itemName + "Button").GetComponent<Button>();
            itemButtons[item] = button;
            button.gameObject.SetActive(false);
        }
        
    }

    private void GenerateRandomItems()
    {
        randomItems.Clear();

        while (randomItems.Count < 3)
        {
            Item randomItem = availableItems[Random.Range(0, availableItems.Count)];
            if (!randomItems.Contains(randomItem))
            {
                randomItems.Add(randomItem);
            }
        }
    }

    private void DisplayItems()
    {
        foreach (var button in itemButtons.Values)
        {
            button.gameObject.SetActive(false);
        }

        foreach (var item in randomItems)
        {
            Button button = itemButtons[item];
            button.GetComponentInChildren<Text>().text = item.itemName;
            button.gameObject.SetActive(true);
        }
    }

    public void BuyItem(Item item)
    {
        if (item.isPurchased)
        {
            // Dacă itemul a fost deja cumpărat, oferă un damage boost
            //PlayerStats.instance.IncreaseDamage(item.damageBoost);
        }
        else
        {
            item.isPurchased = true;
            //PlayerInventory.instance.AddItemToInventory(item);
        }

        shopUI.SetActive(false); // Închide shop-ul după achiziție
    }

    public void OpenShop()
    {
        GenerateRandomItems();
        DisplayItems();
        shopUI.SetActive(true); // Activează UI-ul shop-ului
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
