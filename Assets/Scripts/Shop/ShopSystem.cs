using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ShopSystem : MonoBehaviour
{
    [System.Serializable]
    public class Item
    {
        public Sprite image;      
        public string title;       
        public string description; 
        public float price;        
    }

    public List<Item> items = new List<Item>(); 
    public GameObject itemPrefab;             
    public Transform itemContainer;           
    public int itemsToDisplay = 3;                      
    void Start()
    {
        PopulateItemList(); 
        GenerateShopItems();
        DisplayItems();
    }

    void PopulateItemList()
    {
        items.Add(new Item
        {
            image = Resources.Load<Sprite>("Health"),
            title = "Health",
            description = "Adds an extra amount of health",
            price = 50f
        });

        items.Add(new Item
        {
            image = Resources.Load<Sprite>("fireball"),
            title = "Fireball",
            description = "Grants you the ability to shoot fireballs",
            price = 60f
        });

        items.Add(new Item
        {
            image = Resources.Load<Sprite>("speed"), 
            title = "Speed",
            description = "Grants you extra speed",
            price = 20f
        });

        items.Add(new Item
        {
            image = Resources.Load<Sprite>("shield"),
            title = "Shield",
            description = "Grants you the shield effect",
            price = 40f
        });
    }

    void GenerateShopItems()
    {
        if (items.Count < itemsToDisplay)
        {
            Debug.LogWarning("Nu există suficiente elemente în listă pentru afișare!");
            return;
        }

        List<Item> randomItems = new List<Item>();

        while (randomItems.Count < itemsToDisplay)
        {
            int randomIndex = Random.Range(0, items.Count);
            Item randomItem = items[randomIndex];

            if (!randomItems.Contains(randomItem)) 
            {
                randomItems.Add(randomItem);
            }
        }

        foreach (Item item in randomItems)
        {
            GameObject newItem = Instantiate(itemPrefab, itemContainer);

            Image itemImage = newItem.transform.Find("Image").GetComponent<Image>();
            itemImage.sprite = item.image;

            Text titleText = newItem.transform.Find("Title").GetComponent<Text>();
            titleText.text = item.title;

            Text descriptionText = newItem.transform.Find("Description").GetComponent<Text>();
            descriptionText.text = item.description;

            Text priceText = newItem.transform.Find("Price").GetComponent<Text>();
            priceText.text = $"Price: {item.price:F2} ";

            Button itemButton = newItem.transform.Find("Button").GetComponent<Button>();
            itemButton.onClick.AddListener(() => OnItemPurchase(item));
        }
    }

    // Selectează 3 iteme aleatorii
    void DisplayItems()
    {
        var randomItems = items.OrderBy(x => Random.value).Take(3).ToList();

        foreach (var item in randomItems)
        {
            // Instanțiezi un prefab de item
            GameObject newItem = Instantiate(itemPrefab, itemContainer);

            // Setează imaginea
            Image itemImage = newItem.transform.Find("Image").GetComponent<Image>();
            if (itemImage != null) itemImage.sprite = item.image;

            // Setează titlul
            Text itemTitle = newItem.transform.Find("Title").GetComponent<Text>();
            if (itemTitle != null) itemTitle.text = item.title;

            // Setează descrierea
            Text itemDescription = newItem.transform.Find("Description").GetComponent<Text>();
            if (itemDescription != null) itemDescription.text = item.description;

            // Setează prețul
            Text itemPrice = newItem.transform.Find("Price").GetComponent<Text>();
            if (itemPrice != null) itemPrice.text = "$" + item.price.ToString("F2");
        }
    }


    void OnItemPurchase(Item item)
    {
        Debug.Log($"Bought: {item.title} pentru {item.price:F2}!");
        // Aici poți adăuga funcționalitatea pentru cumpărare
    }
}
