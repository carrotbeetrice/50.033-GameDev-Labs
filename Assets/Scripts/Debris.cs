using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    public GameConstants gameConstants;
    private float speed = 1;
    private Rigidbody2D debrisBody;
    private Vector3 scaler;

    // Start is called before the first frame update
    void Start()
    {
        scaler = transform.localScale / (float) 30;
        debrisBody = GetComponent<Rigidbody2D>();

        int horizontalDirection = UnityEngine.Random.Range(0, 2) * 2 - 1;
        debrisBody.AddForce(new Vector2(horizontalDirection * speed, 0.2f), ForceMode2D.Impulse);
        StartCoroutine(ScaleOut());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ScaleOut()
    {
        Vector2 direction = new Vector2(Random.Range(-1.0f, 1.0f), 1);
        debrisBody.AddForce(direction.normalized * gameConstants.breakDebrisForce, ForceMode2D.Impulse);
        debrisBody.AddTorque(gameConstants.breakDebrisTorque, ForceMode2D.Impulse);

        // Wait for next frame
        yield return null;

        // Render for 0.5 seconds
        for (int step = 0; step < gameConstants.breakTimeStep; step++)
        {
            this.transform.localScale = this.transform.localScale - scaler;
            yield return null;
        }
        Destroy(gameObject);
    }
    
}
