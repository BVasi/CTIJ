using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformItemSpawner : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SpawnItem());
    }

    private IEnumerator SpawnItem()
    {
        while (true)
        {
            if (_spawnedItem != null)
            {
                yield return new WaitForSeconds(ITEM_AVAILABILITY_CHECK_INTERVAL);
                continue;
            }

            yield return new WaitForSeconds(Random.Range(MIN_ITEM_SPAWN_TIME, MAX_ITEM_SPAWN_TIME));
            _spawnedItem = Instantiate(_genericItemPrefab, transform.position + SPAWN_OFFSET,
                Quaternion.identity);
            _spawnedItem.transform.SetParent(transform);
        }
    }

    [SerializeField] private GameObject _genericItemPrefab;
    private GameObject _spawnedItem;
    private readonly Vector3 SPAWN_OFFSET = new Vector3(0f, 0.6f, 0f);
    private const float ITEM_AVAILABILITY_CHECK_INTERVAL = 1f;
    private const float MIN_ITEM_SPAWN_TIME = 5f;
    private const float MAX_ITEM_SPAWN_TIME = 15f;
    private const int FIRST_ITEM_INDEX = 0;
}
