using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    private Animator anim;
    private CatControllerScript playerMovement;

    [SerializeField] private AudioSource deathSoundEffect;

    private void Start()
    {
        playerMovement = GetComponent<CatControllerScript>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("spike"))
        {
            Die();
        }
    }

    private void Die()
    {
        deathSoundEffect.Play();
        anim.SetTrigger("death");
        playerMovement.enabled = false;
        
    }
}


