using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollector : MonoBehaviour
{
    public static PlayerCollector instance;

    private TimeSpan timePlaying;
    private bool timerGoing;

    private float elapsedTime;

    public static int totalScore;
    public static bool isAlive;
    private Animator anim;
    private CatControllerScript playerMovement;
    public LevelChanger endGame;
    
    private int fishes;
    float score;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text fishText;
    [SerializeField] private AudioSource collectionSoundEffect;
    [SerializeField] private AudioSource finishSound;
    [SerializeField] private AudioSource deathSoundEffect;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerMovement = GetComponent<CatControllerScript>();    
        endGame = GameObject.FindGameObjectWithTag("nextLevel").GetComponent<LevelChanger>();  
        anim = GetComponent<Animator>();
        fishes = 0;
        timerGoing = true;
        elapsedTime = 0f;
        score = 0;
        totalScore = 0;
        isAlive=true;

    }

    private void Update ()
    {
        if (isAlive)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            // string timePlayingStr = "TIME: " + timePlaying.ToString("mm':'ss'.'ff");
            score += elapsedTime;
            totalScore = (int)Mathf.Round(score);
            scoreText.text = "SCORE: " + totalScore.ToString();
            fishText.text = "FISHES: " + fishes + " / 20";
        }      
    }

    void CalculateScore ()
    {
        double t = timePlaying.Seconds;
        float playtime = (float)t;
            if (playtime < 5f) // 2,000
            {
                // score += playtime * 1f;
                score += playtime * 1f;
            }
            else if (playtime < 10f) //1,000
            {
                score += playtime * 1.5f;
            }
            else if (playtime < 15f) //300
            {
                score += playtime * 2f;
            }
            else if (playtime < 20f) //160
            {
                score += playtime * 1.5f;
            }
            else
            {
                score += playtime * 3f;
            }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("fish"))
        {
            fishes++;
            collectionSoundEffect.Play();
            CalculateScore();
        }

        if (fishes == 20)
        {
            totalScore = (int)Mathf.Round(score) + (1000 * (20-fishes));
            timerGoing = false;
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
        totalScore = (int)Mathf.Round(score) + (1000 * (20-fishes));
        deathSoundEffect.Play();
        anim.SetTrigger("death");
        playerMovement.enabled = false;
        isAlive = false;
        scoreText.text = "FINAL SCORE: " + totalScore.ToString();
        endGame.LoadGameOver();
    }
}
