using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollector : MonoBehaviour
{
    public static int totalScore;
    public static bool isAlive;
    private Animator anim;
    private CatControllerScript playerMovement;
    public LevelChanger endGame;
    
    private int fishes = 0;
    float timeLapsed;
    float score;
    // float test;

    private int f1 = 0;
    private int f2 = 0;
    private int f3 = 0;
    private int f4 = 0;
    private int f5 = 0;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text fishText;
    [SerializeField] private AudioSource collectionSoundEffect;
    [SerializeField] private AudioSource finishSound;
    [SerializeField] private AudioSource deathSoundEffect;

    private void Start()
    {
        playerMovement = GetComponent<CatControllerScript>();    
        endGame = GameObject.FindGameObjectWithTag("nextLevel").GetComponent<LevelChanger>();  
        anim = GetComponent<Animator>();
    }

    private void Awake ()
    {
        timeLapsed += Time.deltaTime * 1;
        score = 0;
        totalScore = 0;
        isAlive=true;
        // test = 0;

        if (timeLapsed < 10f) // 2,000
        {
            f1 = fishes + 1;
        }
        else if (timeLapsed < 25f) //1,000
        {
            f2 = fishes - f1 + 1;
        }
        else if (timeLapsed < 60f) //300
        {
            f3 = fishes - f1 - f2 + 1;
        }
        else if (timeLapsed < 80f) //160
        {
            f4 = fishes - f3 - f2 -f1 + 1;
        }
        else
        {
            f5 = fishes - f4 - f3 - f2 - f1 + 1;
        }

    }

    private void Update ()
    {
        if (isAlive)
        {
            totalScore = (int)Mathf.Round(score);
            scoreText.text = "SCORE: " + totalScore.ToString();
            fishText.text = "FISHES: " + fishes + " / 20";

            if (timeLapsed < 10f) // 2,000
            {
                score += (timeLapsed * 200 * f1);
            }
            else if (timeLapsed < 25f) //1,000
            {
                score += (timeLapsed * 40 * f2);
            }
            else if (timeLapsed < 60f) //300
            {
                score += (timeLapsed * f3);
            }
            else if (timeLapsed < 80f) //160
            {
                score += (timeLapsed * f4);
            }
            else
            {
                score += (timeLapsed * f5);
            }
        }      
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("fish"))
        {
            collectionSoundEffect.Play();
            fishes++;
        }

        if (fishes == 20)
        {
            finishSound.Play();
            fishText.text = "FISHES: 20 / 20";
            anim.SetTrigger("win");
            playerMovement.enabled = false;
            isAlive = false;
            scoreText.text = "FINAL SCORE: " + totalScore.ToString();
            endGame.LoadGameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("spike"))
        {
            Die();
        }
    }

    public void Die()
    {
        deathSoundEffect.Play();
        anim.SetTrigger("death");
        playerMovement.enabled = false;
        isAlive = false;
        scoreText.text = "FINAL SCORE: " + totalScore.ToString();
        endGame.LoadGameOver();
    }
}
