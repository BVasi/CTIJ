using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
        _ridigBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (_player == null)
        {
            return;
        }
        if (IsPlayerInAttackRange())
        {
            _ridigBody.velocity = new Vector2(NO_MOVEMENT, _ridigBody.position.y);
            _animator.SetFloat(SPEED_ANIMATOR_PARAMETER_NAME, Mathf.Abs(_ridigBody.velocity.x));
            TriggerAttack();
            return;
        }
        float distanceToPlayer = (_player.transform.position.x - transform.position.x);
        _ridigBody.velocity = new Vector2(Mathf.Sign(distanceToPlayer) * _speed, _ridigBody.velocity.y);
        _animator.SetFloat(SPEED_ANIMATOR_PARAMETER_NAME, Mathf.Abs(_ridigBody.velocity.x));
        if ((distanceToPlayer > NO_MOVEMENT) && !_isFacingRight)
        {
            Flip();
        }
        else if ((distanceToPlayer < NO_MOVEMENT) && _isFacingRight)
        {
            Flip();
        }
    }

    private void TriggerAttack()
    {
        _animator.SetTrigger(ATTACKING_ANIMATOR_TRIGGER_NAME);
    }

    private void ApplyAttackDamage()
    {
        if (_player == null || !IsPlayerInAttackRange())
        {
            return;
        }
        _player.GetComponent<PlayerStats>().TakeDamage(_attackDamage);
    }

    private bool IsPlayerInAttackRange()
    {
        if (_player == null)
        {
            return false;
        }
        return Vector2.Distance(transform.position, _player.transform.position) <= _attackRange;
    }

    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        _isFacingRight = !_isFacingRight;
    }

    private GameObject _player;
    private Rigidbody2D _ridigBody;
    private Animator _animator;
    private float _speed = 6f;
    private float _attackRange = 1.5f;
    private int _attackDamage = 10;
    private bool _isFacingRight = false;
    private const float NO_MOVEMENT = 0f;
    private const string PLAYER_TAG = "Player";
    private const string SPEED_ANIMATOR_PARAMETER_NAME = "speed";
    private const string ATTACKING_ANIMATOR_TRIGGER_NAME = "attack";
}
