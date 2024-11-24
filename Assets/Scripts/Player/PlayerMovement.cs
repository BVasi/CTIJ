using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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
    }

    void FixedUpdate()
    {
        float xFacingDirection = Input.GetAxis(HORIZONTAL);
        _ridigBody.velocity = new Vector2(xFacingDirection * _speed, _ridigBody.velocity.y);
        _animator.SetFloat(SPEED_ANIMATOR_PARAMETER_NAME, Mathf.Abs(_ridigBody.velocity.x));
        if ((xFacingDirection > NO_MOVEMENT_THRESHOLD) && !_facingRight)
        {
            Flip();
        }
        else if ((xFacingDirection < NO_MOVEMENT_THRESHOLD) && _facingRight)
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
    }

    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        _facingRight = !_facingRight;
    }

    private Rigidbody2D _ridigBody;
    private Animator _animator;
    private float _speed = 8f;
    private float _jumpForce = 8f;
    private bool _isGrounded = true;
    private bool _facingRight = false;

    private const float NO_MOVEMENT_THRESHOLD = 0f;
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private const string GROUND_TAG = "Ground";
    private const string SPEED_ANIMATOR_PARAMETER_NAME = "speed";
    private const string JUMPING_ANIMATOR_PARAMETER_NAME = "isJumping";
}
