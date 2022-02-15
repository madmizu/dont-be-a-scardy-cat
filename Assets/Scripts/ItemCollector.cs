using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private int fishes = 0;
    private Animator anim;
    private BoxCollider2D collider;


    private void Start()
    {
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            Destroy(gameObject, .9f);
            collider.enabled = false;
            anim.SetBool("popped", true);
            fishes++;
        }
    }
}
