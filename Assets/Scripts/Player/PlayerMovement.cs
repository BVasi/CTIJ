using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public void IncreaseSpeed(int amount) //to do: refactor inside playerstats
    {
        _speed += amount;
    }

    public void IncreaseDamage(int amount) //to do: refactor inside playerstats
    {
        _attackDamage += amount;
    }

    void Start()
    {
        _ridigBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _ridigBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _isGrounded = false;
            _animator.SetBool(JUMPING_ANIMATOR_PARAMETER_NAME, true);
        }
        if (Input.GetMouseButtonDown(MSB1))
        {
            TriggerAttack();
        }
    }

    void FixedUpdate()
    {
        float xFacingDirection = Input.GetAxis(HORIZONTAL);
        _ridigBody.velocity = new Vector2(xFacingDirection * _speed, _ridigBody.velocity.y);
        _animator.SetFloat(SPEED_ANIMATOR_PARAMETER_NAME, Mathf.Abs(_ridigBody.velocity.x));
        if ((xFacingDirection > NO_MOVEMENT) && !_isFacingRight)
        {
            Flip();
        }
        else if ((xFacingDirection < NO_MOVEMENT) && _isFacingRight)
        {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            _isGrounded = true;
            _animator.SetBool(JUMPING_ANIMATOR_PARAMETER_NAME, false);
        }
        if (collision.gameObject.TryGetComponent<PlatformMovement>(out PlatformMovement platformMovement))
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlatformMovement>(out PlatformMovement platformMovement))
        {
            transform.SetParent(null);
        }
    }

    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        _isFacingRight = !_isFacingRight;
    }

    private void TriggerAttack()
    {
        _animator.SetTrigger(ATTACKING_ANIMATOR_TRIGGER_NAME);
    }

    private void ApplyAttackDamage()
    {
        Collider2D[] objectsInAttackRange = Physics2D.OverlapCircleAll(transform.position, _overallAttackRange);
        foreach (var objectInAttackRange in objectsInAttackRange)
        {
            if (objectInAttackRange.CompareTag(ENEMY_TAG) && IsEnemyInFrontOfPlayer(objectInAttackRange.transform)
                && IsEnemyInHeightAttackRange(objectInAttackRange.transform))
            {
                objectInAttackRange.GetComponent<EnemyStats>().TakeDamage(_attackDamage);
            }
        }
    }

    private bool IsEnemyInFrontOfPlayer(Transform enemyTransform)
    {
        if (_isFacingRight)
        {
            return enemyTransform.position.x > transform.position.x;
        }
        else
        {
            return enemyTransform.position.x < transform.position.x;
        }
    }

    private bool IsEnemyInHeightAttackRange(Transform enemyTransform)
    {
        return Mathf.Abs(enemyTransform.position.y - transform.position.y) <= _heightAttackRange;
    }

    private Rigidbody2D _ridigBody;
    private Animator _animator;
    private float _speed = 8f;
    private float _jumpForce = 8f;
    private float _overallAttackRange = 1.5f;
    private float _heightAttackRange = 1f;
    private int _attackDamage = 10;
    private bool _isGrounded = true;
    private bool _isFacingRight = false;

    private const float NO_MOVEMENT = 0f;
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private const string GROUND_TAG = "Ground";
    private const string ENEMY_TAG = "Enemy";
    private const string SPEED_ANIMATOR_PARAMETER_NAME = "speed";
    private const string JUMPING_ANIMATOR_PARAMETER_NAME = "isJumping";
    private const string ATTACKING_ANIMATOR_TRIGGER_NAME = "attack";

    private const int MSB1 = 0;
}
