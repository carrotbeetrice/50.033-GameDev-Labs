using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablePatrolController : MonoBehaviour
{
    // public Texture t;
    public GameConstants gameConstants;
    private int moveRight;
    private Rigidbody2D mushroomBody;

    // Start is called before the first frame update
    void Start()
    {
        // scaler = transform.localScale / 30f;
        mushroomBody = GetComponent<Rigidbody2D>();
        moveRight = Random.Range(0, 2) == 0 ? -1 : 1; // Randomise initial direction
        mushroomBody.AddForce(new Vector2(0, gameConstants.consumableinitialUpSpeed), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        mushroomBody.velocity = new Vector2(moveRight * gameConstants.consumablePatrolSpeed, mushroomBody.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.CompareTag("Obstacles")) 
        {
            moveRight *= -1;
        }
        // else if (col.gameObject.CompareTag("Player") && !collected)
        // {
        //     collected = true;
        //     CentralManager.centralManagerInstance.addPowerup(t, 0, this);
        //     StartCoroutine(ScaleIn());
        // }
    }
}
