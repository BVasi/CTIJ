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
    public string imageName;
    public string title;
    public string description;
    public int price;
    public StatType statType;
    public int improvedAmount;
}

public class ShopManager : MonoBehaviour
{
    public void OnNextWaveButtonPressed()
    {
        GameManager.Instance.UpdateGameState(GameState.MainGamePlay);
    }

    void Start()
    {
        GenerateShopStats();
    }

    private void GenerateShopStats()
    {
        if (ALL_STATS.Count < STATS_TO_DISPLAY)
        {
            return;
        }
        List<Stat> randomStats = ALL_STATS.OrderBy(x => Random.value).Take(STATS_TO_DISPLAY).ToList();
        while (randomStats.Count < STATS_TO_DISPLAY)
        {
            int randomIndex = Random.Range(FIRST_STAT_INDEX, ALL_STATS.Count);
            Stat randomStat = ALL_STATS[randomIndex];
            randomStats.Add(randomStat);
        }
        foreach (Stat stat in randomStats)
        {
            GameObject newStat = Instantiate(statPrefab, statContainer);
            SetStatUI(newStat, stat);
        }
    }

    private void SetStatUI(GameObject statObject, Stat stat)
    {
        Image statImage = statObject.transform.Find(STAT_OBJECT_IMAGE).GetComponent<Image>();
        if (statImage != null)
        {
            statImage.sprite = Resources.Load<Sprite>(stat.imageName);
        }
        Text statTitle = statObject.transform.Find(STAT_OBJECT_TITLE).GetComponent<Text>();
        if (statTitle != null)
        {
            statTitle.text = stat.title;
        }
        Text statDescription = statObject.transform.Find(STAT_OBJECT_DESCRIPTION).GetComponent<Text>();
        if (statDescription != null)
        {
            statDescription.text = stat.description;
        }
        Text statPrice = statObject.transform.Find(STAT_OBJECT_PRICE).GetComponent<Text>();
        if (statPrice != null)
        {
            statPrice.text = "$" + stat.price.ToString("F2");
        }
        Button statButton = statObject.transform.Find(STAT_OBJECT_BUTTON).GetComponent<Button>();
        statButton.onClick.AddListener(() => OnStatPurchase(statObject, stat));
    }

    private void OnStatPurchase(GameObject statObject, Stat stat)
    {
        if (!GameManager.Instance.HasCoins(stat.price))
        {
            return;
        }
        GameManager.Instance.SpendCoins(stat.price);
        PlayerController.Instance.ImproveStat(stat.statType, stat.improvedAmount);
        Destroy(statObject);
    }

    [SerializeField] private GameObject statPrefab;
    [SerializeField] private Transform statContainer;

    private readonly List<Stat> ALL_STATS = new List<Stat>()
    {
        new Stat
        {
            imageName = HEALTH_IMAGE_NAME,
            title = HEALTH_TITLE,
            description = HEALTH_DESCRIPTION,
            price = HEALTH_PRICE,
            statType = StatType.Health,
            improvedAmount = HEALTH_IMPROVEMENT_AMOUNT
        },
        new Stat
        {
            imageName = SPEED_IMAGINE_NAME,
            title = SPEED_TITLE,
            description = SPEED_DESCRIPTION,
            price = SPEED_PRICE,
            statType = StatType.Speed,
            improvedAmount = SPEED_IMPROVEMENT_AMOUNT
        },
        new Stat
        {
            imageName = DAMAGE_IMAGE_NAME,
            title = DAMAGE_TITLE,
            description = DAMAGE_DESCRIPTION,
            price = DAMAGE_PRICE,
            statType = StatType.Damage,
            improvedAmount = DAMAGE_IMPROVEMENT_AMOUNT
        }
    };
    private const int FIRST_STAT_INDEX = 0;
    private const int STATS_TO_DISPLAY = 3;

    private const string HEALTH_IMAGE_NAME = "health";
    private const string HEALTH_TITLE = "Health";
    private const string HEALTH_DESCRIPTION = "Adds extra health";
    private const int HEALTH_PRICE = 0;
    private const int HEALTH_IMPROVEMENT_AMOUNT = 15;
    private const string SPEED_IMAGINE_NAME = "speed";
    private const string SPEED_TITLE = "Speed";
    private const string SPEED_DESCRIPTION = "Grants you extra speed";
    private const int SPEED_PRICE = 0;
    private const int SPEED_IMPROVEMENT_AMOUNT = 2;
    private const string DAMAGE_IMAGE_NAME = "damage";
    private const string DAMAGE_TITLE = "Damage";
    private const string DAMAGE_DESCRIPTION = "Grants you extra damage";
    private const int DAMAGE_PRICE = 0;
    private const int DAMAGE_IMPROVEMENT_AMOUNT = 10;

    private const string STAT_OBJECT_IMAGE = "Image";
    private const string STAT_OBJECT_TITLE = "Title";
    private const string STAT_OBJECT_DESCRIPTION = "Description";
    private const string STAT_OBJECT_PRICE = "Price";
    private const string STAT_OBJECT_BUTTON = "Button";
}
