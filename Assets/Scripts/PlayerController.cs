using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    // Inputs
    private float inputX;
    private float inputY;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpForce;

    // State

    private bool isGrounded;


    // Components
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Transform groundPoint;
    [SerializeField]
    private LayerMask groundLayers;
    [SerializeField]
    SpriteRenderer spriteRenderer;
    
    void Update()
    {
        isGrounded = CheckGrounded();
    
        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
        if(inputX != 0)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

        if(inputX > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (inputX < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private bool CheckGrounded()
    {
        return Physics2D.OverlapCircle(groundPoint.position, .2f, groundLayers);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump!");
        if(context.performed && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        
    }


    
    public void Move(InputAction.CallbackContext context)
    {
        // Only called when change in movement
        Debug.Log("Move!");

        inputX = context.ReadValue<Vector2>().x;
        Debug.Log("InputX changed to: " + inputX);
        inputY = context.ReadValue<Vector2>().y;
        
    }

}