using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D marioBody;
    public float maxSpeed = 10;
    public float upSpeed = 10;
    private bool onGroundState = true;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    public Transform enemyLocation;
    public Text scoreText;
    private int score = 0;
    private bool countScoreState = false;
    private Animator marioAnimator;
    private AudioSource marioAudio;
    public ParticleSystem dustCloud;

    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        marioAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update xSpeed to match Mario's current x speed
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
        
        // Toggle state
        if (Input.GetKeyDown("a") && faceRightState) {
            faceRightState = false;
            marioSprite.flipX = true;
            if (Mathf.Abs(marioBody.velocity.x) > 1.0) {
                marioAnimator.SetTrigger("onSkid");
            }
        }
        if (Input.GetKeyDown("d") && !faceRightState) {
            faceRightState = true;
            marioSprite.flipX = false;
            if (Mathf.Abs(marioBody.velocity.x) > 1.0) {
                marioAnimator.SetTrigger("onSkid");
            }
        }

        // When jumping, Gomba is near Mario and we haven't registered our score
        if (!onGroundState && countScoreState) 
        {
            if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
            {
                countScoreState = false;
                score++;
                Debug.Log(score);
            }
        }
    }

    private void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");

        if (Mathf.Abs(moveHorizontal) > 0) {
            Vector2 movement = new Vector2(moveHorizontal, 0);

            // Let Mario accelerate until maxSpeed
            if (marioBody.velocity.magnitude < maxSpeed) {
                marioBody.AddForce(movement * speed);
            }
            
        }

        // Stop Mario when key "a" or "d" is lifted up
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d")) {
            marioBody.velocity = Vector2.zero;
        }

        // Make Mario jump on spacebar press
        if (Input.GetKeyDown("space") && onGroundState) {
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            marioAnimator.SetBool("onGround", onGroundState);
            countScoreState = true; // Check if Gomba is underneath when jumping
            PlayJumpSound();
        }
           
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")  || other.gameObject.CompareTag("Obstacles"))
        {
            Debug.Log("Mario collided with " + other.gameObject.tag);
            dustCloud.Play();
            onGroundState = true; // Back on ground
            marioAnimator.SetBool("onGround", onGroundState);
            countScoreState = false; // Reset score state
            // marioBody.velocity = Vector2.zero;
            scoreText.text = "Score: " + score.ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Mario collided with Gomba");

            // Game over on collision; restart scene
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    void PlayJumpSound() {
        marioAudio.PlayOneShot(marioAudio.clip);
    }
}
