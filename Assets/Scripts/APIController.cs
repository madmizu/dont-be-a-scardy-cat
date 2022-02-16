using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;

public class APIController : MonoBehaviour
{
    public Text nameText;
    public Text pointsText;
    public TextMeshProUGUI[] highscoreArray;    
    public TextMeshProUGUI[] usernameArray;     
    private readonly string baseScoreURL = "https://scardey-cat-data.herokuapp.com/scores";

    private void Start()
    {
        nameText.text = APIPost.playName + " SCORED";
        pointsText.text = PlayerCollector.totalScore.ToString() + " POINTS";
        foreach (TextMeshProUGUI highscoreText in highscoreArray)
            {
                highscoreText.text = "";
            }
        foreach (TextMeshProUGUI usernameText in usernameArray)
            {
                usernameText.text = "";
            }
        StartCoroutine(GetHighScores());
    }

    IEnumerator GetHighScores()
    {



        string highScoreURL = baseScoreURL;
        UnityWebRequest scoreInfoRequest = UnityWebRequest.Get(highScoreURL);
        yield return scoreInfoRequest.SendWebRequest();
        // error handling:
            if (scoreInfoRequest.isNetworkError || scoreInfoRequest.isHttpError)
            {
            //only shows error in console. !!! NEED to add error messages to render on page!!!
            Debug.LogError(scoreInfoRequest.error);
            yield break;
            }

        JSONNode scoreInfo = JSON.Parse(scoreInfoRequest.downloadHandler.text);
        string[] allScores = new string[scoreInfo.Count];
        string[] allUsernames = new string[scoreInfo.Count];
            for (int i = 0; i < scoreInfo.Count; i++)
            {
                allScores[i] = scoreInfo[i]["score"];
                if (scoreInfo[i]["name"])
                    {
                        allUsernames[i] = scoreInfo[i]["name"];
                    }
                else
                    {
                        allUsernames[i] = scoreInfo[i]["user"]["username"];
                    }
            }
        for (int i = 0; i < 10; i++)
        {
            highscoreArray[i].text = allScores[i];
        }
        for (int i = 0; i < 10; i++)
        {
            usernameArray[i].text = allUsernames[i];
        }
    }
}
