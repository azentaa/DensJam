using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    


    [SerializeField] private float _wallSlidingSpeed;
    [SerializeField] private int _jumpPower = 1;
    [SerializeField] private float _fallRatio = 1;

    private Rigidbody2D _rigidbody;

    private Vector2 _gravityVec;
    private Vector2 _wallJumpPower = new Vector2(8f, 16f);

    private float horizontal;
    private float _coyoteTime = 0.2f;
    private float _coyoteTimeCounter;
    private float _bufferTime = 0.2f;
    private float _bufferTimeCounter;
    private float _wallJumpingDirection = 0.2f;
    private float _wallJumpingTime;
    private float _wallJumpingCounter;
    private float _wallJumpingDuration;

    private bool _isWallSliding;
    private bool _isWallJumping;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public Transform wallCheck;
    public LayerMask wallLayer;

    private void Start()
    {
        _gravityVec = new Vector2(0, -Physics2D.gravity.y);
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (isGrounded())
        {
            _coyoteTimeCounter = _coyoteTime;
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            _bufferTimeCounter = _bufferTime;
        }
        else
        {
            _bufferTimeCounter -= Time.deltaTime;
        }

        if(_bufferTimeCounter > 0f && _coyoteTimeCounter > 0f)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpPower);
            _bufferTimeCounter = 0f;
        }

        if (Input.GetButtonUp("Jump") && _rigidbody.velocity.y > 0f)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.4f);
            _coyoteTimeCounter = 0f;
        }

        if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.velocity -= _gravityVec * _fallRatio * Time.deltaTime;
        }

        WallSlide();
    }
    
    private bool isGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.9f, 0.3f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }


    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.5f, wallLayer);
    }

    private void WallSlide()
    {
        
        if (IsWalled() && !isGrounded())
        {
            _isWallSliding = true;

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Mathf.Clamp(_rigidbody.velocity.y, -_wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            _isWallSliding = false;
        }
    }
    private void WallJump()
    {
        if (_isWallSliding)
        {
            _isWallJumping = false;
            _wallJumpingDirection = -transform.localScale.x;
            _wallJumpingCounter = _wallJumpingTime;
        }
        else
        {
            _wallJumpingCounter -= Time.deltaTime;
        }

        if(Input.GetButtonDown("Jump") && _wallJumpingCounter > 0f)
        {
            _isWallJumping = true;
            _rigidbody.velocity = new Vector2(_wallJumpPower.x * _wallJumpingDirection, _wallJumpPower.y);
            _wallJumpingCounter = 0f;
            //if(transform.localScale.x != _wallJumpingDirection)
            //{

            //}
        }
    }
}
