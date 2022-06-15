using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    void Awake() 
    {
        // Prevent game from starting before button is pressed
        Time.timeScale = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Callback for start button
    public void StartButtonClicked()
    {
        foreach (Transform eachChild in transform)
        {
            // Iterate over UI children st they will not be rendered anymore
            if (eachChild.name != "Score")
            {
                Debug.Log("Child found. Name: " + eachChild.name);
                // Disable them
                eachChild.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }
}
