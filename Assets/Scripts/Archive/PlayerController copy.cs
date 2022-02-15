using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerCopy : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private float moveSpeed;
    private float jumpForce;
    private bool isJumping;
    private float moveHorizontal;
    private float moveVertical;    

    // Start is called before the first frame update
    void Start()
    {
        // 'gameObject' with lower case 'g' is a reference to a the game object this script is attached to in Unity
        // .GetComponent<>(); inside <> you list the component.
        rb2d = gameObject.GetComponent<Rigidbody2D>();

        moveSpeed = 2f;
        jumpForce = 5f;
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if the player is typing on a key, we want to grab that click. 
        // Input is a property. .GetAxisRaw this grabs the type of input we specify in the ()
        // we are setting the value of 'moveHorizontal' to the user's input, specifically the keys that would move an object left and right or a and d (i.e. 'horizontal')
        // You can change what "horizontal" means in the Unity Project Settings
        moveHorizontal =Input.GetAxisRaw("Horizontal");
        moveVertical =Input.GetAxisRaw("Jump");
    }

    void FixedUpdate ()
    {
        // anything involving physics, we put in FixedUpdate.
        // this could be 0 instead of 0.1f.
        // This like say if we are moving left or if we are moving right.
        if(moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            // not we add force to our object
            // Vector2(x axis, y axis) == How far to move side to side and up and down
            // .AddForce applies Time.DeltaTime as a default in its Forcemode. Time.DeltaTime is the time it took to render the last frame
            // ForceMode2D.Impulse has instantaneous change in movement. 
            rb2d.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
        }
        if(!isJumping && moveVertical > 0.1f)
        {
            rb2d.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
        } 
        //Raycast = we take our player, shoot a ray that we cant see, is shot towards the ground
        // boxcast shoots a box downward.
    }

    //this is to stop character from continually jumping.
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            isJumping = false;
        }
    }

    void onTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            isJumping = true;
        }
    }
}
