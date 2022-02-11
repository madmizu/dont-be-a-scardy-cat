// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // declaring public variables will allow the variable to be accessed and changed in unity editor
    public float JumpForce = 1;
    public float MovementSpeed = 1;

    private Rigidbody2D rb2d;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Awake ()
    {
        spriteRenderer = GetComponent<SpriteRenderer> ();    
        animator = GetComponent<Animator> ();
    }

    private void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // referencing the default horizontal input axis (i.e. 'a', 'd', 'left arrow', 'right arrow')
        // returns 1 for 'd' and 'right arrow' keys
        // returns -1 for 'a' and 'left arrow' keys
        var movement = Input.GetAxis("Horizontal");
        // Vector2 takes in two parameters: x-axis & y-axis. Vector 3 would have a 3rd parameter for z-axis
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

        if(Input.GetButtonDown("Jump") && Mathf.Abs(rb2d.velocity.y) < 0.001f)
        {
            rb2d.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        }
    }
}
