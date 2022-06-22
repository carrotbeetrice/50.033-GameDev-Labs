using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMonitor : MonoBehaviour
{
    public IntVariable marioScore;
    public Text text;

    public void Start() 
    {
        UpdateScore();
    }

    public void UpdateScore()
    {
        text.text = "Score: " + marioScore.Value.ToString();
    }
}
