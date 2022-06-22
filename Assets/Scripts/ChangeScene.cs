using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public AudioSource changeSceneSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            changeSceneSound.PlayOneShot(changeSceneSound.clip);
            StartCoroutine(LoadNextAsyncScene());
        }
    }

    IEnumerator LoadNextAsyncScene()
    {
        yield return new WaitUntil(() => !changeSceneSound.isPlaying);
        CentralManager.centralManagerInstance.changeScene();
    }

}
