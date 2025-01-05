using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public enum StatType
{
    Health,
    Speed,
    Damage
}
[System.Serializable]

public class Stat
{
    public Sprite image;
    public string title;
    public string description;
    public int price;
    public StatType statType;
    public int improvedAmount;
}

public class ShopManager : MonoBehaviour
{
    public List<Stat> stats = new List<Stat>();
    public GameObject statPrefab;
    public Transform statContainer;
    public int statsToDisplay = 3;
    void Start()
    {
        PopulateStatList();
        GenerateShopStats();
        DisplayStats();
    }

    void PopulateStatList()
    {
        stats.Add(new Stat
        {
            image = Resources.Load<Sprite>("health"),
            title = "Health",
            description = "Adds an extra amount of health",
            price = 0,
            statType = StatType.Health,
            improvedAmount = 15
        });
        stats.Add(new Stat
        {
            image = Resources.Load<Sprite>("speed"),
            title = "Speed",
            description = "Grants you extra speed",
            price = 0,
            statType = StatType.Speed,
            improvedAmount = 2
        });
        stats.Add(new Stat
        {
            image = Resources.Load<Sprite>("damage"), //to do: change for some kind of damage sprite
            title = "Damage",
            description = "Grants you extra damage",
            price = 0,
            statType = StatType.Damage,
            improvedAmount = 10
        });
    }

    void GenerateShopStats()
    {
        if (stats.Count < statsToDisplay)
        {
            Debug.LogWarning("Not Enough Elements!");
            return;
        }

        List<Stat> randomStats = new List<Stat>();

        while (randomStats.Count < statsToDisplay)
        {
            int randomIndex = Random.Range(0, stats.Count);
            Stat randomStat = stats[randomIndex];

            if (!randomStats.Contains(randomStat))
            {
                randomStats.Add(randomStat);
            }
        }

        foreach (Stat stat in randomStats)
        {
            GameObject newStat = Instantiate(statPrefab, statContainer);

            Image statImage = newStat.transform.Find("Image").GetComponent<Image>();
            statImage.sprite = stat.image;

            Text titleText = newStat.transform.Find("Title").GetComponent<Text>();
            titleText.text = stat.title;

            Text descriptionText = newStat.transform.Find("Description").GetComponent<Text>();
            descriptionText.text = stat.description;

            Text priceText = newStat.transform.Find("Price").GetComponent<Text>();
            priceText.text = $"Price: {stat.price:F2} ";

            Button statButton = newStat.transform.Find("Button").GetComponent<Button>();
            statButton.onClick.AddListener(() => OnStatPurchase(stat));
        }
    }


    void DisplayStats()
    {
        var randomStats = stats.OrderBy(x => Random.value).Take(3).ToList();

        foreach (var stat in randomStats)
        {

            GameObject newStat = Instantiate(statPrefab, statContainer);

            Image statImage = newStat.transform.Find("Image").GetComponent<Image>();
            if (statImage != null) statImage.sprite = stat.image;

            Text statTitle = newStat.transform.Find("Title").GetComponent<Text>();
            if (statTitle != null) statTitle.text = stat.title;

            Text statDescription = newStat.transform.Find("Description").GetComponent<Text>();
            if (statDescription != null) statDescription.text = stat.description;

            Text statPrice = newStat.transform.Find("Price").GetComponent<Text>();
            if (statPrice != null) statPrice.text = "$" + stat.price.ToString("F2");
        }
    }


    void OnStatPurchase(Stat stat)
    {
        if (!GameManager.Instance.HasCoins(stat.price))
        {
            Debug.Log("Sarakie mare");
            GameManager.Instance.UpdateGameState(GameState.MainGamePlay);
            return;
        }
        GameManager.Instance.SpendCoins(stat.price);
        PlayerController.Instance.ImproveStat(stat.statType, stat.improvedAmount);
        GameManager.Instance.UpdateGameState(GameState.MainGamePlay); //to do: make this after user presses next wave button (rn cant buy more than 1 item)
    }
}
