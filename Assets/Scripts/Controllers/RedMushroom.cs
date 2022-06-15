using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedMushroom : MonoBehaviour, ConsumableInterface
{
    public Texture t;
    public GameConstants gameConstants;
    private float speed = 4;
    private int moveRight;
    private Rigidbody2D mushroomBody;
    private bool collected = false;
    private Vector3 scaler;

    // Start is called before the first frame update
    void Start()
    {
        // scaler = transform.localScale / gameConstants.
        mushroomBody = GetComponent<Rigidbody2D>();
        // random left or right
        moveRight = Random.Range(0, 2) == 0 ? -1 : 1; // Randomise initial direction
        mushroomBody.AddForce(new Vector2(0, 0.15f), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        mushroomBody.velocity = new Vector2(moveRight * speed, mushroomBody.velocity.y);
    }

    protected IEnumerator ScaleIn()
    {
		float shrinkStepper =  1.0f / gameConstants.shrinkTimeStep;

		for (int i = 0; i < gameConstants.enlargeTimeStep; i++){
			gameObject.transform.localScale = new Vector3(this.transform.localScale.x + gameConstants.enlargeTimeStepper, this.transform.localScale.y  +  gameConstants.enlargeTimeStepper, this.transform.localScale.z);

			// make sure enemy is still above ground
			gameObject.transform.position = new Vector3(this.transform.position.x, gameConstants.groundSurface  +  GetComponent<SpriteRenderer>().bounds.extents.y, this.transform.position.z);
			yield return null;
		}

        for (int i = 0; i < gameConstants.shrinkTimeStep; i++){
			gameObject.transform.localScale = new Vector3(this.transform.localScale.x  -  shrinkStepper, this.transform.localScale.y  -  shrinkStepper, this.transform.localScale.z);

			// make sure enemy is still above ground
			gameObject.transform.position = new Vector3(this.transform.position.x, gameConstants.groundSurface  +  GetComponent<SpriteRenderer>().bounds.extents.y, this.transform.position.z);
			yield return null;
		}
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.CompareTag("Obstacles")) 
        {
            Debug.Log("Mushroom hit pipe!");
            moveRight *= -1;
        }
        else if (col.gameObject.CompareTag("Player") && !collected)
        {
            collected = true;
            // Update UI
            CentralManager.centralManagerInstance.addPowerup(t, 1, this);
            StartCoroutine(ScaleIn());
        }
    }

    public void consumedBy(GameObject player)
    {
		// Give player speed boost
        Debug.Log("Consuming red mushroom");
		player.GetComponent<PlayerController>().maxSpeed  *=  2;
		StartCoroutine(removeEffect(player));
	}

	IEnumerator removeEffect(GameObject player
    ){
		yield return new WaitForSeconds(5.0f);
		player.GetComponent<PlayerController>().maxSpeed  /=  2;
    }
}
