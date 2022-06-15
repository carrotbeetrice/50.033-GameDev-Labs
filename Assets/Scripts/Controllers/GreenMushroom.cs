using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenMushroom : MonoBehaviour, ConsumableInterface
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
        scaler = transform.localScale / (float) 30;
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

    IEnumerator ScaleIn()
    {
        int addSteps = 1;
        float addStepper = 0.1f;
        int shrinkSteps = 4;
		float shrinkStepper =  1.0f/(float) shrinkSteps;

		for (int i = 0; i < addSteps; i++)
        {
			this.transform.localScale = new Vector3(this.transform.localScale.x + addStepper, this.transform.localScale.y  +  addStepper, this.transform.localScale.z);
			this.transform.position = new Vector3(this.transform.position.x, gameConstants.groundSurface  +  GetComponent<SpriteRenderer>().bounds.extents.y, this.transform.position.z);
			yield return null;
		}

        for (int i = 0; i < shrinkSteps; i++)
        {
			this.transform.localScale = new Vector3(this.transform.localScale.x  -  shrinkStepper, this.transform.localScale.y  -  shrinkStepper, this.transform.localScale.z);
			this.transform.position = new Vector3(this.transform.position.x, gameConstants.groundSurface  +  GetComponent<SpriteRenderer>().bounds.extents.y, this.transform.position.z);
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
            CentralManager.centralManagerInstance.addPowerup(t, 0, this);
            StartCoroutine(ScaleIn());
        }
    }
    
    public void consumedBy(GameObject player)
    {
        // Give player jump boost
        Debug.Log("Consuming green mushroom");
        player.GetComponent<PlayerController>().upSpeed += 10;
        StartCoroutine(removeEffect(player));
    }

    IEnumerator removeEffect(GameObject player)
    {
        yield return new WaitForSeconds(5.0f);
        player.GetComponent<PlayerController>().upSpeed -= 10;
    }
}
