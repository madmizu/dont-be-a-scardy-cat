using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class APIPost : MonoBehaviour
{
    string highscore_url = "https://scardycat.herokuapp.com/scores";
    public static string playName;
    string score = PlayerCollector.totalScore.ToString();
    string userId = "0";


    public void ReadStringInput(string str)
    {
        playName = str;
        Debug.Log(playName);
    }

    public void onSubmit ()
    {
        StartCoroutine(SendRequest());
    }
    // Use this for initialization
    IEnumerator SendRequest()
    {
        // Create a form object for sending high score data to the server
        WWWForm form = new WWWForm();

        // Assuming the perl script manages high scores for different games
        form.AddField( "user_id", userId );

        // The name of the player submitting the scores
        form.AddField( "name", playName );

        // The score
        form.AddField( "score", score );

        // Create a download object
        var download = UnityWebRequest.Post(highscore_url, form);

        // Wait until the download is done
        yield return download.SendWebRequest();

        if (download.result != UnityWebRequest.Result.Success)
        {
            print( "Error downloading: " + download.error );
        }
        else
        {
            // show the highscores
            Debug.Log(download.downloadHandler.text);
        }
    }


}