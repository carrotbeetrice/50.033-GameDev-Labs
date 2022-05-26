using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    public float speed = 10;
    private Rigidbody2D mushroomBody;
    public float upSpeed = 10;
    private bool onGroundState = false;

    // Start is called before the first frame update
    void Start()
    {
        // Spring out of the box once instantiated

        // Pick a random direction to start moving
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Mushroom collided with Mario");
            mushroomBody.velocity = Vector2.zero; // Stop moving
        }
        else if (other.gameObject.CompareTag("Obstacles") || other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Mushroom collided with " + other.gameObject.tag);
        }
        else if (other.gameObject.CompareTag("Pipe"))
        {
            Debug.Log("Mushroom collided with pipe");
            float moveHorizontal = Input.GetAxis("Horizontal");
            if (Mathf.Abs(moveHorizontal) > 0)
            {
                Vector2 movement = new Vector2(moveHorizontal, 0);
                // send help
            }
        }
    }
}
