using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public SpringJoint2D springJoint;
    public GameObject consumablePrefab;
    public SpriteRenderer spriteRenderer;
    public Sprite usedQuestionBox;
    private bool hit = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player") && !hit) {
            Debug.Log("Collided with Mario!");
            hit = true;
            // Add more upwards force when collision is detected
            rigidBody.AddForce(new Vector2(0, rigidBody.mass * 20), ForceMode2D.Impulse);
            // Spawn mushroom prefab slightly above the box
            Instantiate<GameObject>(consumablePrefab, new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, this.transform.position.z), Quaternion.identity);
            StartCoroutine(DisableHittable());
        }    
    }

    bool ObjectMovedAndStopped()
    {
        return  Mathf.Abs(rigidBody.velocity.magnitude)<0.01;
    }

    IEnumerator DisableHittable()
    {
        if (!ObjectMovedAndStopped())
        {
            yield return new WaitUntil(() => ObjectMovedAndStopped());
        }

        //continues here when the ObjectMovedAndStopped() returns true
        spriteRenderer.sprite = usedQuestionBox; // change sprite to be "used-box" sprite
        rigidBody.bodyType = RigidbodyType2D.Static; // make the box unaffected by Physics

        //reset box position
        // this.transform.localPosition = Vector3.zero;
        springJoint.enabled = false; // disable spring
    }
}
