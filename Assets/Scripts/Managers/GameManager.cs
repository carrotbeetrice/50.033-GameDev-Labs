using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text score;
    // public GameObject spawnManagerObject;
    private int playerScore = 0;
    public delegate void gameEvent();
    public static event gameEvent OnPlayerDeath;
    public static event gameEvent OnIncreaseScore;
    // private SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        // spawnManager = spawnManagerObject.GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseScore()
    {
        playerScore += 1;
        Debug.Log("Player score: " + playerScore.ToString());
        score.text = "SCORE: " + playerScore.ToString();
        OnIncreaseScore();
        // spawnManager.spawnNewEnemy();
    }

    public void damagePlayer()
    {
        OnPlayerDeath();
    }


}
