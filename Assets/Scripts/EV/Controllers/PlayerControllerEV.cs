using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerEV : MonoBehaviour
{
    public IntVariable marioUpSpeed;
    public IntVariable marioMaxSpeed;
    public CustomCastEvent onCastPowerup;
    public GameConstants gameConstants;
    public ParticleSystem dustCloud;
    private float force;
    private Rigidbody2D marioBody;
    private bool onGroundState = true;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    private Animator marioAnimator;
    private AudioSource marioJumpAudio;
    private AudioSource marioDeathAudio;
    private bool isDead = false;


    // Start is called before the first frame update
    void Start()
    {
         // Set to be 30 FPS
        Application.targetFrameRate = 30;

        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();

        AudioSource[] sounds = GetComponents<AudioSource>();
        marioJumpAudio = sounds[0];
        marioDeathAudio = sounds[1];

        marioUpSpeed.SetValue(gameConstants.playerMaxJumpSpeed);
        marioMaxSpeed.SetValue(gameConstants.playerStartingMaxSpeed);
        force = gameConstants.playerDefaultForce;
    }

    // Update is called once per frame
    void Update()
    {
         // Update xSpeed to match Mario's current x speed
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
        
        // Toggle state
        if (Input.GetKeyDown("a") && faceRightState) 
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (Mathf.Abs(marioBody.velocity.x) > 1.0) 
            {
                marioAnimator.SetTrigger("onSkid");
            }
        }
        if (Input.GetKeyDown("d") && !faceRightState) 
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if (Mathf.Abs(marioBody.velocity.x) > 1.0) 
            {
                marioAnimator.SetTrigger("onSkid");
            }
        }
        if (Input.GetKeyDown("z"))
        {
            onCastPowerup.Invoke(KeyCode.Z);
        }
        if (Input.GetKeyDown("x"))
        {
            onCastPowerup.Invoke(KeyCode.X);
        }

    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");

            if (Mathf.Abs(moveHorizontal) > 0) {
                float direction = faceRightState ? 1.0f : -1.0f;
                Vector2 movement = new Vector2(force * direction, 0);
                if (marioBody.velocity.magnitude < marioMaxSpeed.Value) {
                    marioBody.AddForce(movement);
                }   
            }

            if (Input.GetKeyDown("space") && onGroundState)
            {
                marioBody.AddForce(Vector2.up * marioUpSpeed.Value, ForceMode2D.Impulse);
                onGroundState = false;
                marioAnimator.SetBool("onGround", onGroundState);
                PlayJumpSound();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Obstacles"))
        {
            dustCloud.Play();
            onGroundState = true;
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

    void PlayJumpSound() {
        marioJumpAudio.PlayOneShot(marioJumpAudio.clip);
    }

    public void PlayerDiesSequence()
    {
        isDead = true;
        marioDeathAudio.PlayOneShot(marioDeathAudio.clip);
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(dead());
    }

    IEnumerator dead() 
    {
        float stepper = 1.0f / 5.0f;

        for (int i = 0; i < 5; i++)
        {
            this.transform.localScale 
                = new Vector3(this.transform.localScale.x, this.transform.localScale.y - stepper, this.transform.localScale.z);
            this.transform.position 
                = new Vector3(this.transform.position.x, gameConstants.groundSurface + GetComponent<SpriteRenderer>().bounds.extents.y, this.transform.position.z);
            yield return null;
        }
        yield break;
    }
}
