using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 7f;
    TouchingDirection touchingDirection;
    Vector2 moveInput;
    Animator anim;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        touchingDirection = GetComponent<TouchingDirection>();
    }

    public float CurrentSpeed
    {
        get {
            if (IsMoving)
            {
                if (IsRunning)
                {
                    return runSpeed;
                }
                else
                {
                    return walkSpeed;

                }
            }
            else
            {
                return 0;
            }
        }
    }

    [SerializeField]
    private bool _isMoving = false;

    public bool IsMoving { get 
        {
            return _isMoving;
        } private set {
            _isMoving = value;
            anim.SetBool(AnimationStrings.isMoving, value);
        } 
    }
    [SerializeField]
    private bool _isRunning = false;

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        private set
        {
            _isRunning = value;
            anim.SetBool(AnimationStrings.isRunning, value);
        }
    }
    [SerializeField]
    private bool _isFacingRight = true;

    public bool IsFacingRight { get 
        {
            return _isFacingRight;
        } private set {
            if(_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight= value;
        }

    }
    
    private void FixedUpdate()
    {
        rb.velocity = new Vector2( moveInput.x * CurrentSpeed, rb.velocity.y);
       anim.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }
    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else
        if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (context.started)
        {
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        }
        else if(context.canceled)
        {
            IsMoving = false;
        }

    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && touchingDirection.IsGrounded)
        {
            anim.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public void OnRunning(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;

        }
        else if(context.canceled)
        {
            IsRunning = false;
        }

    }
}
