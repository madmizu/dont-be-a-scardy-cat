using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Text pointsText;

    public void Start () {
        pointsText.text = PlayerCollector.totalScore.ToString() + " POINTS";
    }
}
