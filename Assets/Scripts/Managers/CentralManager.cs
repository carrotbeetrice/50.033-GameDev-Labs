using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CentralManager : MonoBehaviour
{
    public GameObject gameManagerObject;
    private GameManager gameManager;
    public GameObject powerupManagerObject;
    private PowerupManager powerupManager;
    public static CentralManager centralManagerInstance;

    void Awake() 
    {
        centralManagerInstance = this;    
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();
        powerupManager = powerupManagerObject.GetComponent<PowerupManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseScore()
    {
        gameManager.increaseScore();
    }

    public void damagePlayer()
    {
        gameManager.damagePlayer();
    }

    public void consumePowerup(KeyCode k, GameObject g) {
        powerupManager.consumePowerup(k, g);
    }

    public void addPowerup(Texture t, int i, ConsumableInterface c) {
        powerupManager.addPowerup(t, i, c);
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void changeScene()
    {
        StartCoroutine(LoadNextAsyncScene("MarioLevel2"));
    }

    IEnumerator LoadNextAsyncScene(string sceneName)
    {
        // Loads scene in background while current scene runs
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
