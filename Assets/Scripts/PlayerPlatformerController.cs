using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject {

    // movement speed
    public float maxSpeed = 7;
    // sets the velocity of jump
    public float jumpTakeOffSpeed = 7;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Use this for initialization
    void Awake () 
    {
        spriteRenderer = GetComponent<SpriteRenderer> ();    
        animator = GetComponent<Animator> ();
    }

    // This will be called every frame by the base class (PhysicsObject) to check for input and update animations
    protected override void ComputeVelocity()
    {
        // Need to get values for our target velocity, so we'll reset move at the beginning of each computation.
        Vector2 move = Vector2.zero;
        //set the x value of 'move' based on the control input from the player
        move.x = Input.GetAxis ("Horizontal");
        // Check if player has pressed jump button, and grounded must be true, so the player cannot jump in mid-air (NO DOUBLE JUMPING)
        if (Input.GetButtonDown ("Jump") && grounded) {
            // if player has jumped, we add value to the y axis.
            velocity.y = jumpTakeOffSpeed;
            // if the button has been released, we subtract velocity to allow player to cancel their jump in mid-air:
            } else if (Input.GetButtonUp ("Jump")) 
            {
                // if we are moving upwards adn we release the jump button, we reduce velocity by half.
                if (velocity.y > 0) {
                velocity.y = velocity.y * 0.5f;
                }
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x < 0.1f) : (move.x > 0.1f));
        if (!flipSprite) 
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        animator.SetBool ("grounded", grounded);
        animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }
}