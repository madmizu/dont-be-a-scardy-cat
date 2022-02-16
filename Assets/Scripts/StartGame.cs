using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            LoadStartGame();
        }
    }

    public void LoadStartGame()
    {
        StartCoroutine(LoadLevelInt(SceneManager.GetActiveScene().buildIndex + 1));
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
        transition.SetTrigger("fadeOut");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelName);
    }
}
