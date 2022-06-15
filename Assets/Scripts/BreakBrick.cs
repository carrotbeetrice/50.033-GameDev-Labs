using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBrick : MonoBehaviour
{
    public GameConstants gameConstants;
    private bool broken = false;
    public GameObject debrisPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !broken)
        {
            Debug.Log("Mario collided with breakable");
            broken = true;

            for (int i = 0; i < gameConstants.spawnNumberOfDebris; i++)
            {
                Instantiate<GameObject>(debrisPrefab, transform.position, Quaternion.identity);
            }
            gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<EdgeCollider2D>().enabled = false;
        }
    }
}
