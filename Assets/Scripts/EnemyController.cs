using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float originalX;
    private float maxOffset = 4.0f;
    private float enemyPatroltime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;

    private Rigidbody2D enemyBody;

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        // Get the starting position
        originalX = transform.position.x;
        ComputeVelocity();
    }

    void ComputeVelocity() {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
    }

    void MoveGomba() {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
        {
            MoveGomba();
        }
        else 
        {
            // Change direction
            moveRight *= -1;
            ComputeVelocity();
            MoveGomba();
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Obstacles")) 
        {
            Debug.Log("Bonk");
            moveRight *= -1;
        }
    }
}
