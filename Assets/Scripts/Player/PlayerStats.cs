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
    }

    public void TakeDamage(int damage)
    {
        if (_isShielded)
        {
            _isShielded = false;
            return;
        }
        _health -= damage;
        if (_health < NO_HEALTH)
        {
            Die();
        }

        StartCoroutine(AnimateHealthBar(CalculateHealthPercentage()));
    }

    public void Shield()
    {
        _isShielded = true;
    }

    public void Heal(int amount)
    {
        _health = Mathf.Min(_health + amount, _maxHealth);
        StartCoroutine(AnimateHealthBar(CalculateHealthPercentage()));
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
        float startFillAmount = _healthBar.fillAmount;
        float elapsedTime = 0f;

        while (elapsedTime < ANIMATION_DURATION)
        {
            elapsedTime += Time.deltaTime;
            _healthBar.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, elapsedTime / ANIMATION_DURATION);
            yield return null;
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
    private int _health;
    private int _maxHealth;
    private int _damage;
    private bool _isShielded;
    private const int NO_HEALTH = 0;
    private const float ANIMATION_DURATION = 1f;
}
