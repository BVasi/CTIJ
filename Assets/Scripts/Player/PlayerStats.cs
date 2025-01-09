using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    void Start()
    {
        _health = 100;
        _damage = 10;
        _maxHealth = 100;
        _isShielded = false;
        _audioSource = GetComponent<AudioSource>();
        if (_shieldImage != null)
        {
            _shieldImage.enabled = false;
        }
    }

    public void TakeDamage(int damage)
    {
        if (_isShielded)
        {
            _isShielded = false;
            if (_shieldImage != null)
            {
                _shieldImage.enabled = false;
            }
            return;
        }
        _health -= damage;
        _audioSource.Play(); //to do: find why this plays at the start of the game
        if (_health < NO_HEALTH)
        {
            Die();
        }

        StartCoroutine(AnimateHealthBar(CalculateHealthPercentage()));
    }

    public void Shield()
    {
        _isShielded = true;
        if (_shieldImage != null)
        {
            _shieldImage.enabled = true;
        }
    }

    public void Heal(int amount)
    {
        _health = Mathf.Min(_health + amount, _maxHealth);
        StartCoroutine(AnimateHealthBar(CalculateHealthPercentage()));
    }

    public void IncreaseMaxHealth(int amount)
    {
        _maxHealth += amount;
    }

    public void DecreaseMaxHealth(int amount)
    {
        _maxHealth -= amount;
    }

    public void IncreaseDamage(int valueToIncrease)
    {
        _damage += valueToIncrease;
    }

    public void DecreaseDamage(int valueToDecrease)
    {
        _damage -= valueToDecrease;
    }

    public bool IsFullHealth()
    {
        return _health == _maxHealth;
    }

    public bool IsShielded()
    {
        return _isShielded;
    }

    private IEnumerator AnimateHealthBar(float targetFillAmount)
    {
        if (!_healthBar)
        {
            yield break;
        }
        float startFillAmount = _healthBar.fillAmount;
        float elapsedTime = 0f;
        while (elapsedTime < ANIMATION_DURATION)
        {
            if (!_healthBar)
            {
                yield break;
            }
            elapsedTime += Time.deltaTime;
            _healthBar.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, elapsedTime / ANIMATION_DURATION);
            yield return null;
        }
        if (!_healthBar)
        {
            yield break;
        }
        _healthBar.fillAmount = targetFillAmount;
    }

    private float CalculateHealthPercentage()
    {
        return (float)_health / _maxHealth;
    }

    private void Die()
    {
        GameManager.Instance.UpdateGameState(GameState.Lose);
    }

    [SerializeField] private Image _healthBar; //to do: put this in UIManager and update it there
    [SerializeField] private Image _shieldImage;
    private AudioSource _audioSource; //to do: refactor
    private int _health;
    private int _maxHealth;
    private int _damage;
    private bool _isShielded;
    private const int NO_HEALTH = 0;
    private const int NO_DAMAGE = 0;
    private const float ANIMATION_DURATION = 1f;
}
