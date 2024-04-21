using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontalMovement;
    public float movementSpeed = 2f; 
    public float jumpForce = 50f;
    public float gravityScale = 2f;
    public float gravitySense = 1f;
    public float jumpTimer = 1f;
    private bool pressedJump = false;
    private bool releasedJump = false;
    private bool startTimer = false;
    private float timer;
    public bool isGrounded = false; 
    public bool isTopGrounded = false;
    private bool isInverted= false;
    private float jumpTimeTracker;
    public float jumpTime;
    private bool isJumping;
    bool isDoubleTap = false;
    private float lastTapTime = 0f;
    public float doubleTapTimeThreshold = 0.5f;
    // POWER UPS
    private bool hasSuperSpeed = false;
    private bool isSpeedBoosted = false;
    private float speedBoostDuration = 3f;
    private float speedBoostEndTime = 0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = jumpTimer;
    }

    
    void Update()

    {   // Movimiento en eje X y salto
        float horizontalMovement= Input.GetAxis("Horizontal");
        rb.velocity= new Vector2(horizontalMovement * movementSpeed, rb.velocity.y);
        if (Input.GetButtonDown("Jump") && (isGrounded || isTopGrounded)) {
                    pressedJump = true;
                    isGrounded = false;
                    isTopGrounded = false;
        }

        if (Input.GetButtonUp("Jump")) {
            releasedJump = true;
        }

        if (startTimer) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                releasedJump = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && (isGrounded || isTopGrounded))
        {
            Debug.Log("Cambio de gravedad");
            isInverted= !isInverted;
            gravitySense *= -1;
            rb.gravityScale *= -1;
            isGrounded = false;
            isTopGrounded = false;
            CharacterRotation();
        }

        if (Input.GetButtonDown("Horizontal"))
        {
            if ((Time.time - lastTapTime) < doubleTapTimeThreshold && !isDoubleTap)
            {
                isDoubleTap = true;
            }
            lastTapTime = Time.time;
        }
        if (isSpeedBoosted && Time.time >= speedBoostEndTime)
        {
            isSpeedBoosted = false;
            movementSpeed -= 8;
        }
    }
        private void FixedUpdate() {
        if (pressedJump) {
            StartJump();
        }

        if (releasedJump) {
            StopJump();
        }

        if (isDoubleTap && hasSuperSpeed && !isSpeedBoosted)
        {
            movementSpeed += 8;
            isDoubleTap = false;
            isSpeedBoosted = true;
            speedBoostEndTime = Time.time + speedBoostDuration;
            Debug.Log("Double tap");
        }
    }
        private void StartJump() {
        rb.gravityScale = 0;
        rb.AddForce(new Vector2(0, gravitySense * jumpForce), ForceMode2D.Impulse);
        pressedJump = false;
        startTimer = true;
    }

    private void StopJump() {
        rb.gravityScale = gravityScale * gravitySense;
        releasedJump = false;
        timer = jumpTimer;
        startTimer = false;
    }

    void CharacterRotation(){
        if (gravitySense == -1)
        {
            transform.eulerAngles= new Vector3(0, 0, 180);
        }
        if (gravitySense == 1)
        {
            transform.eulerAngles= new Vector3(0, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {   
            isGrounded= true;
        }
        if (collision.gameObject.tag == "TopGround")
        {
            isTopGrounded= true;
        }
    }

    // POWER UP OBTAIN FUNCTIONS
    public void ObtainSuperSpeed()
    {
        hasSuperSpeed = true;
    }
}
