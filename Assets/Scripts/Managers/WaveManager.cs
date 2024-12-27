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
        _currentWave = INITIALIZATION_VALUE;
        _enemiesPerWave = INITIALIZATION_VALUE;
        _isWaveActive = false;
        _isSpawningComplete = false;
        _timer = GetComponent<Timer>();
        _timer.OnTimerEnd += HandleOnTimerEnd;
        _enemies = new List<GameObject>();
        StartNextWave();
    }

    void Update()
    {
        if (!_isWaveActive)
        {
            return;
        }
        if (IsWaveDead())
        {
            _timer.StopTimer();
            HandleWaveSuccess();
            GameManager.Instance.UpdateGameState(GameState.ShopMenu);
            return;
        }
        if (_timer.HasExpired())
        {
            _timer.StopTimer();
            HandleWaveFailure();
            GameManager.Instance.UpdateGameState(GameState.Lose);
        }
    }

    public void StartNextWave()
    {
        _currentWave++;
        _enemiesPerWave += 5;
        _isSpawningComplete = false;
        StartCoroutine(SpawnEnemiesOverTime());
        _timer.StartTimerForSeconds(Random.Range(MIN_WAVE_TIME, MAX_WAVE_TIME));
        _isWaveActive = true;
    }

    private IEnumerator SpawnEnemiesOverTime() //to do: stop if game over
    {
        for (int enemyIndex = FIRST_ENEMY_INDEX; enemyIndex < _enemiesPerWave; enemyIndex++)
        {
            GameObject enemyToSpawn = _enemyPrefabs[Random.Range(FIRST_ENEMY_INDEX, _enemyPrefabs.Length)];
            GameObject spawnedEnemy = Instantiate(enemyToSpawn,
                ENEMY_POSSIBLE_SPAWNS[Random.Range(FIRST_ENEMY_SPAWN_LOCATION_INDEX, ENEMY_POSSIBLE_SPAWNS.Length)],
                Quaternion.identity);
            _enemies.Add(spawnedEnemy);
            yield return new WaitForSeconds(Random.Range(MIN_SPAWN_DELAY, MAX_SPAWN_DELAY));
        }
        _isSpawningComplete = true;
    }

    private void HandleOnTimerEnd()
    {
        _timer.StopTimer();
        if (IsWaveDead())
        {
            HandleWaveSuccess();
            return;
        }
        HandleWaveFailure();
    }

    private bool IsBossWave()
    {
        return (_currentWave % BOSS_WAVE_INTERVAL) == INITIALIZATION_VALUE;
    }

    private bool IsWaveDead()
    {
        if (!_isSpawningComplete)
        {
            return false;
        }
        foreach (GameObject enemy in _enemies)
        {
            if (enemy != null)
            {
                return false;
            }
        }
        return true;
    }

    private void HandleWaveSuccess()
    {
        _isWaveActive = false;
        _enemies.Clear();
        GameManager.Instance.UpdateGameState(GameState.ShopMenu);
    }

    private void HandleWaveFailure()
    {
        _isWaveActive = false;
        GameManager.Instance.UpdateGameState(GameState.Lose);
    }

    public static WaveManager Instance { get; private set; }
    [SerializeField] private GameObject[] _enemyPrefabs;
    private int _currentWave;
    private int _enemiesPerWave;
    private bool _isWaveActive;
    private bool _isSpawningComplete;
    private List<GameObject> _enemies;
    private Timer _timer;

    private Vector3[] ENEMY_POSSIBLE_SPAWNS = {
        new Vector3(-35.0f, -1.5f, 0.0f),
        new Vector3(35.0f, -1.5f, 0.0f)
    };

    private const float MIN_WAVE_TIME = 45f;
    private const float MAX_WAVE_TIME = 91f;
    private const float MIN_SPAWN_DELAY = 2f;
    private const float MAX_SPAWN_DELAY = 5f;
    private const int FIRST_ENEMY_SPAWN_LOCATION_INDEX = 0;
    private const int FIRST_ENEMY_INDEX = 0;
    private const int BOSS_WAVE_INTERVAL = 5;
    private const int INITIALIZATION_VALUE = 0;
}