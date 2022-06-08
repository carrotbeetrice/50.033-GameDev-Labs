using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{

    private float speed = 4;
    private int currentDirection;
    private Rigidbody2D mushroomBody;

    // Start is called before the first frame update
    void Start()
    {
        mushroomBody = GetComponent<Rigidbody2D>();
        // random left or right
        int randbinary = UnityEngine.Random.Range(0, 2);
        if (randbinary == 0) {
            currentDirection = 1;
        } else {
            currentDirection = -1;
        }
        mushroomBody.AddForce(new Vector2(0, 0.15f), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        mushroomBody.velocity = new Vector2(currentDirection * speed, mushroomBody.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.CompareTag("Obstacles")) 
        {
            Debug.Log("Mushroom hit pipe!");
            currentDirection *= -1;
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Mushroom hit Mario!");
            // mushroomBody.velocity = Vector2.zero;
            // currentDirection = 0;
        }
    }

    void OnBecameInvisible()
    {
        Debug.Log("RIP Mushroom");
        Destroy(gameObject);
    }
}
