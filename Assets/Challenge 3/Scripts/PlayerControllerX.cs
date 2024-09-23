using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    
    private Animator playerAnim;
    private AudioSource playerAudio;
    public Rigidbody playerRb;
    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public float floatForce = 10f;
    public float gravityModifier = 1.5f;
    public float maxHeight = 15f;
    public bool isOnGround = true; 
    public bool gameOver;


    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        isOnGround = transform.position.y < maxHeight;
    
        
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }

    }

}
