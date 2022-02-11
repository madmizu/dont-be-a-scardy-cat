// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class CatControllerScript : MonoBehaviour
{
    public float MovementSpeed;
    public float JumpForce;

    private Rigidbody2D rb2d;
    private SpriteRenderer sprite;
    private Animator anim;

    private float movement;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb2d.velocity.y) < 0.001f)
        {
            rb2d.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            anim.SetBool("jumping", true);
        } else if (Mathf.Abs(rb2d.velocity.y) < 0.001f)
        {
            anim.SetBool("jumping", false);
        }

    UpdateAnimationState();

    }

    private void UpdateAnimationState ()
    {
        if (movement > 0f)
        {
            anim.SetBool("running", true);
            sprite.flipX = false;
        }
        else if (movement < 0f)
        {
            anim.SetBool("running", true);
            sprite.flipX = true;
        }
        else
        {
            anim.SetBool("running", false);
        }

        // if (Input.GetButtonDown("Jump"))
        // {
        //     anim.SetBool("jumping", true);
        // }
        // else
        // {
        //     anim.SetBool("jumping", false);
        // }
    }
}
