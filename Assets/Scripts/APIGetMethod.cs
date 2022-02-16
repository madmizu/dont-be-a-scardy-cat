using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class APIGetMethod : MonoBehaviour
{
    InputField outputArea;

    void Start()
    {
        outputArea = GameObject.Find("OutputArea").GetComponent<InputField>();
        //Need to call submit button "ButtonEnterScore"
        GameObject.Find("GetButton").GetComponent<Button>().onClick.AddListener(GetData);
    }

    void GetData() => StartCoroutine(GetData_Coroutine());

    IEnumerator GetData_Coroutine()
    {
        outputArea.text = "Loading...";
        string uri = "https://scardey-cat-data.herokuapp.com/scores";
        using(UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();
            if(request.isNetworkError || request.isHttpError)
            {
                outputArea.text = request.error;
            }
                else   
                {
                    outputArea.text = request.downloadHandler.text;    
                }
        
        }
    }
}
