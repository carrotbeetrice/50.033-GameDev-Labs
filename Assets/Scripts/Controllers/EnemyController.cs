using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameConstants gameConstants;
    private float originalX;
    private int moveRight;
    private Vector2 velocity;
    // private bool killed = false;

    private Rigidbody2D enemyBody;

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        originalX = transform.position.x; // Get the starting position
        moveRight = Random.Range(0, 2) == 0 ? -1 : 1; // Randomise initial direction
        ComputeVelocity(); // Compute initial velocity
        GameManager.OnPlayerDeath += EnemyRejoice;
    }

    void ComputeVelocity() {
        velocity = new Vector2((moveRight) * gameConstants.maxOffset / gameConstants.enemyPatrolTime, 0);
    }

    void MoveEnemy() {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < gameConstants.maxOffset)
        {
            MoveEnemy();
        }
        else 
        {
            // Change direction
            moveRight *= -1;
            ComputeVelocity();
            MoveEnemy();
        }
    }


    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {

            // Check if collides on top
            float yOffset = other.transform.position.y - this.transform.position.y;
            if (yOffset > 0.75f)
            {
                KillSelf();
            }
            else
            {
                // Hurt player
                CentralManager.centralManagerInstance.damagePlayer();
            }
        }
    }

    void EnemyRejoice()
    {
        Debug.Log("RIP Mario");
        // TODO: Animate this
        enemyBody.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
    }

    void KillSelf()
    {
        CentralManager.centralManagerInstance.increaseScore();
        StartCoroutine(flatten());
        Debug.Log("Enemy killed");
    }

    IEnumerator flatten()
    {
        Debug.Log("Flatten starts");
        float stepper = 1.0f / gameConstants.flattenTimeStep;

        for (int i = 0; i < gameConstants.flattenTimeStep; i++)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y - stepper, this.transform.localScale.z);

            // Make sure enemy is still above ground
            this.transform.position = new Vector3(this.transform.position.x, gameConstants.groundSurface + GetComponent<SpriteRenderer>().bounds.extents.y, this.transform.position.z);
            yield return null;
        }
        Debug.Log("Flatten ends");
        this.gameObject.SetActive(false);
        Debug.Log("Enemy returned to pool");
        yield break;
    }
}
