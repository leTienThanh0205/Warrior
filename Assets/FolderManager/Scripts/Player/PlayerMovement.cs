using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour
{
    
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 12f;
    public float doubleJumpForce = 8f;
    public float horizontalJumpForce = 6;
    public float horizontal;
    public bool jumpPressed;
    public int direction = 1;
    public bool canMove = true;
    private bool doubleJump;

    [Header("Dash")]
    public float dashDistance = 5f;    // Khoảng cách tốc biến
    public float dashDuration = 0.2f;   // Thời gian tốc biến
    public KeyCode dashKey = KeyCode.C; // Nút để thực hiện tốc biến
    [SerializeField] private TrailRenderer tr;
    private bool isDashing = false;     // Kiểm tra trạng thái tốc biến

    [Header("Ground Check")]
    public Transform groundCheck;
    public float footOffest = 0.4f;
    public float groundDistance = 0.1f;
    public LayerMask groundLayer;
    public bool onGround;

    [Header("Wall")]
    public bool onWall;
    public Vector3 wallOffset;
    public float wallRadius;
    public float maxFallSpeed = -1;
    public float wallJumpDuration = 0.25f;
    public bool jumpFromWall;
    public float jumpFinish;
    public LayerMask wallLayer;

    private bool clearInputs;
    private Rigidbody2D rb;
    TouchingDirection touchingDirection;
    Vector2 moveInput;
    Animator anim;

    public float CurrentSpeed
    {
        get {
            if (canMove)
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
    /*[SerializeField]
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

    }*/
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        touchingDirection = GetComponent<TouchingDirection>();
    }
    private void Update()
    {
        CheckInputs();
        PhysicsCheck();
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2( moveInput.x * CurrentSpeed, rb.velocity.y);
        
        anim.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);

        GroundMovement();
        AirMovement();
        clearInputs = true;
    }

    ////WallJump
    void GroundMovement()
    {
        if (!canMove)
            return;

        float x = horizontal * CurrentSpeed;

        rb.velocity = new Vector2(x, rb.velocity.y);

        if (x * direction < 0f)
            Flip();

        anim.SetBool(AnimationStrings.isMoving, horizontal != 0);

    }
    void AirMovement()
    {
        if (jumpPressed && onGround)
        {
            jumpPressed = false;
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

        }
        else if (jumpPressed && onWall && !onGround)
        {
            canMove = false;
            jumpFinish = Time.time + wallJumpDuration;
            jumpPressed = false;
            jumpFromWall = true;
            Flip();

            rb.velocity = Vector2.zero;

            rb.AddForce(new Vector2(horizontalJumpForce * direction, jumpForce), ForceMode2D.Impulse);
        }
    }

    //Dash
    private IEnumerator Dash()
    {
        isDashing = true; // Đánh dấu trạng thái tốc biến
        Vector3 startPosition = transform.position; // Vị trí bắt đầu
        Vector3 targetPosition = startPosition + new Vector3(dashDistance * Mathf.Sign(transform.localScale.x), 0, 0); // Vị trí mục tiêu

        float elapsedTime = 0f;
        tr.emitting = true;
        // Di chuyển đến vị trí mục tiêu trong thời gian tốc biến
        while (elapsedTime < dashDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / dashDuration));
            elapsedTime += Time.deltaTime; // Cập nhật thời gian đã trôi qua
            yield return null; // Chờ đợi frame tiếp theo
        }

        transform.position = targetPosition; // Đặt vị trí cuối cùng
        isDashing = false; // Kết thúc trạng thái tốc biến
        tr.emitting = false;
    }

    void CheckInputs()
    {
        if (clearInputs)
        {
            jumpPressed = false;
            clearInputs = false;
        }


        jumpPressed = jumpPressed || Input.GetButtonDown("Jump");

        if (canMove)
            horizontal = Input.GetAxis("Horizontal");

        if (jumpFromWall)
        {
            if (Time.time > jumpFinish)
            {
                jumpFromWall = false;
            }
        }

        if (!jumpFromWall && !canMove)
        {
            if (Input.GetAxis("Horizontal") != 0 || onGround)
            {
                canMove = true;
            }
        }

    }
    void Flip()
    {
        direction *= -1;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    void PhysicsCheck()
    {
        onGround = false;
        onWall = false;

        RaycastHit2D leftFoot = Raycast(groundCheck.position + new Vector3(-footOffest, 0), Vector2.down, groundDistance, groundLayer);
        RaycastHit2D rightFoot = Raycast(groundCheck.position + new Vector3(footOffest, 0), Vector2.down, groundDistance, groundLayer);

        if (leftFoot || rightFoot)
        {
            onGround = true;
        }

        bool rightWall = Physics2D.OverlapCircle(transform.position + new Vector3(wallOffset.x, 0), wallRadius, wallLayer);
        bool leftWall = Physics2D.OverlapCircle(transform.position + new Vector3(-wallOffset.x, 0), wallRadius, wallLayer);

        if (rightWall || leftWall )
        {
            onWall = true;
        }

        if (onWall)
        {
            if (rb.velocity.y < maxFallSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
            }
        }

        anim.SetBool("isGrounded", onGround);
        anim.SetBool("isWall", onWall);

    }

    public RaycastHit2D Raycast(Vector2 origin, Vector2 rayDirection, float length, LayerMask mask, bool drawRay = true)
    {

        //Send out the desired raycasr and record the result
        RaycastHit2D hit = Physics2D.Raycast(origin, rayDirection, length, mask);
        //If we want to show debug raycasts in the scene...
        if (drawRay)
        {
            Color color = hit ? Color.red : Color.green;
            //...and draw the ray in the scene view
            Debug.DrawRay(origin, rayDirection * length, color);
        }
        //...determine the color based on if the raycast hit...

        //Return the results of the raycast
        return hit;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position + new Vector3(wallOffset.x, 0), wallRadius);
        Gizmos.DrawWireSphere(transform.position + new Vector3(-wallOffset.x, 0), wallRadius);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && touchingDirection.IsGrounded && doubleJump == false)
        {
            anim.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            doubleJump = true;
        }else if(context.started && doubleJump== true && !touchingDirection.IsGrounded)
        {
            anim.SetTrigger(AnimationStrings.doubleJumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);
            doubleJump = false;
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            anim.SetTrigger(AnimationStrings.attackTrigger);
        }
    }
    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            anim.SetTrigger(AnimationStrings.rangedAttackTrigger);
        }
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started && !isDashing)
        {
            anim.SetTrigger(AnimationStrings.dashTrigger);

            StartCoroutine(Dash());
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
/*private void SetFacingDirection(Vector2 moveInput)
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
    else if (context.canceled)
    {
        IsMoving = false;
    }

}*/