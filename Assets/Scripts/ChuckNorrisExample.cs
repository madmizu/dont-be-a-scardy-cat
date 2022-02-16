using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChuckNorrisExample : MonoBehaviour
{
    public Text jokeText;

    public void NewJoke()
    {
        // set joke text to value returned
        Joke j = APIHelper.GetNewJoke();
        jokeText.text = j.value;
    }
}
