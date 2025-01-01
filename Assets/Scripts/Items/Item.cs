using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Health,
    TemporarySpeed,
    TemporaryDamage,
    Shield
}

public class Item : MonoBehaviour
{
    void Start()
    {
        ItemType[] allItemTypes = (ItemType[])System.Enum.GetValues(typeof(ItemType));
        _itemType = allItemTypes[Random.Range(FIRST_ITEM_TYPE_INDEX, allItemTypes.Length)];
        SetItemSpriteBasedOnItemType();
    }

    private void SetItemSpriteBasedOnItemType()
    {
        if (!_itemSprites.ContainsKey(_itemType))
        {
            Destroy(gameObject);
            return;
        }
        Sprite spriteToLoad = Resources.Load<Sprite>(_itemSprites[_itemType]);
        if (spriteToLoad == null)
        {
            Destroy(gameObject);
            return;
        }
        GetComponent<SpriteRenderer>().sprite = spriteToLoad;
        AdjustSpriteScale();
    }

    private void AdjustSpriteScale()
    {
        float widthRatio = DESIRED_WIDTH / GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        float heightRatio = DESIRED_HEIGHT / GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        transform.localScale = new Vector3(widthRatio, heightRatio, transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            playerController.AddItem(_itemType);
            Destroy(gameObject);
        }
    }

    private ItemType _itemType;

    private static readonly Dictionary<ItemType, string> _itemSprites = new Dictionary<ItemType, string>()
    {
        {ItemType.Health, HEALTH_ITEM_SPRITE_NAME},
        {ItemType.Shield, SHIELD_ITEM_SPRITE_NAME},
        {ItemType.TemporaryDamage, DAMAGE_ITEM_SPRITE_NAME},
        {ItemType.TemporarySpeed, SPEED_ITEM_SPRITE_NAME}
    };
    private const int FIRST_ITEM_TYPE_INDEX = 0;
    private const string HEALTH_ITEM_SPRITE_NAME = "health";
    private const string SHIELD_ITEM_SPRITE_NAME = "shield";
    private const string DAMAGE_ITEM_SPRITE_NAME = "damage";
    private const string SPEED_ITEM_SPRITE_NAME = "speed";
    private const float DESIRED_WIDTH = 0.7f;
    private const float DESIRED_HEIGHT = 0.7f;
}
