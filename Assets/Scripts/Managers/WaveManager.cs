using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        currentWave_ = INITIALIZATION_VALUE;
        enemiesPerWave_ = INITIALIZATION_VALUE;
        isWaveActive_ = false;
        timer_ = GetComponent<Timer>();
        timer_.OnTimerEnd += HandleOnTimerEnd;
        enemies_ = new List<GameObject>();
        StartNextWave();
    }

    void Update()
    {
        if (!isWaveActive_)
        {
            return;
        }
        isWaveActive_ = false;
        if (IsWaveDead())
        {
            enemies_.Clear();
            timer_.StopTimer();
            GameManager.Instance.UpdateGameState(GameState.ShopMenu);
            return;
        }
        if (timer_.HasExpired())
        {
            GameManager.Instance.UpdateGameState(GameState.Lose);
        }
    }

    public void StartNextWave()
    {
        currentWave_++;
        enemiesPerWave_ += 5;
        if (IsBossWave())
        {
            //to do: handle boss logic (change enemystats, generate less enemies)
            return;
        }
        for (int enemyIndex = FIRST_ENEMY_INDEX; enemyIndex < enemiesPerWave_; enemyIndex++)
        {
            GameObject enemyToSpawn = enemyPrefabs_[Random.Range(FIRST_ENEMY_INDEX, enemyPrefabs_.Length)];
            //to do: change some enemystats
            GameObject spawnedEnemy = Instantiate(enemyToSpawn,
                ENEMY_POSSIBLE_SPAWNS[Random.Range(FIRST_ENEMY_SPAWN_LOCATION_INDEX, ENEMY_POSSIBLE_SPAWNS.Length)],
                Quaternion.identity);
            enemies_.Add(spawnedEnemy);
        }
        timer_.StartTimerForSeconds(Random.Range(MIN_WAVE_TIME, MAX_WAVE_TIME));
        isWaveActive_ = true;
    }

    private void HandleOnTimerEnd()
    {
        timer_.OnTimerEnd -= HandleOnTimerEnd;
        isWaveActive_ = false;
        if (IsWaveDead())
        {
            enemies_.Clear();
            GameManager.Instance.UpdateGameState(GameState.ShopMenu);
            return;
        }
        GameManager.Instance.UpdateGameState(GameState.Lose);
    }

    private bool IsBossWave()
    {
        return (currentWave_ % BOSS_WAVE_INTERVAL) == INITIALIZATION_VALUE;
    }

    private bool IsWaveDead()
    {
        foreach (GameObject enemy in enemies_)
        {
            if (enemy != null)
            {
                return false;
            }
        }
        return true;
    }

    public static WaveManager Instance { get; private set; }
    [SerializeField] private GameObject[] enemyPrefabs_;
    private int currentWave_;
    private int enemiesPerWave_;
    private bool isWaveActive_;
    private List<GameObject> enemies_;
    private Timer timer_;

    private Vector3[] ENEMY_POSSIBLE_SPAWNS = {
        new Vector3(-35.0f, -1.5f, 0.0f),
        new Vector3(35.0f, -1.5f, 0.0f)
    };

    private const float MIN_WAVE_TIME = 45f;
    private const float MAX_WAVE_TIME = 91f;
    private const int FIRST_ENEMY_SPAWN_LOCATION_INDEX = 0;
    private const int FIRST_ENEMY_INDEX = 0;
    private const int BOSS_WAVE_INTERVAL = 5;
    private const int INITIALIZATION_VALUE = 0;
}