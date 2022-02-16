using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    float delay = 2f;

    // Update is called once per frame
    // void Update()
    // {
    //     if(Input.GetMouseButtonDown(0))
    //     {
    //         LoadNextLevel();
    //     }
    // }

    public void LoadRestart ()
    {
        StartCoroutine(LoadLevelName("1-StartScreen"));
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevelInt(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadSubmitHighScore()
    {
        StartCoroutine(LoadLevelName("SubmitScore"));
    }

    public void SeeHighScores()
    {
        StartCoroutine(LoadLevelName("5-HighScores"));
    }

    public void LoadGameOver()
    {
        StartCoroutine(LoadLevelName("3-GameOver"));
    }

    public void Quit()
    {
        Debug.Log("APPLICATION QUIT!");
        Application.Quit();
    }

    IEnumerator LoadLevelInt(int levelIndex)
    {
        transition.SetTrigger("fadeOut");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator LoadLevelName(string levelName)
    {
        if (levelName == "3-GameOver")
        {
            yield return new WaitForSeconds(transitionTime);
        }
            transition.SetTrigger("fadeOut");
            yield return new WaitForSeconds(transitionTime);
            SceneManager.LoadScene(levelName);
    }
}
