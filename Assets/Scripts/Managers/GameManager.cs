using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public Text score;
    private int playerScore = 0;
    public delegate void gameEvent();
    public static event gameEvent OnPlayerDeath;
    public static event gameEvent OnIncreaseScore;
    private static GameManager _instance;

    public static GameManager Instance
    {
        get {return _instance;}
    }

    public override void Awake() {
        base.Awake();
        
        // Check if instance is not this, means it has been set before, return
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        
        _instance = this; // Otherwise, create this instance
        DontDestroyOnLoad(this.gameObject); // Preserve object on loading scene
    }

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
