using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontalMovement;
    private int maxHealth = 5;
    private int currentHealth;
    public float invulnerabilityDuration = 1f;
    private bool isInvulnerable = false;
    private float invulnerabilityTimer; 
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
    public AudioSource gravitySwiftSound;
    public AudioSource walkingSound;
    bool stoppedWalkingSound;

    // POWER UPS
    private bool hasSuperSpeed = false;
    private bool isSpeedBoosted = false;
    private float speedBoostDuration = 3f;
    private float speedBoostEndTime = 0f;
    public bool hasSuperAttraction = false;
    private bool isSuperAttractionActive = false;
    private bool hasDoubleJump = false;
    private bool hasDoubleJumped = false;

    [Header("Animation")]
    private Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = jumpTimer;
        currentHealth = maxHealth;
    }

    
    void Update()

    {   // Movimiento en eje X y salto
        float horizontalMovement= Input.GetAxis("Horizontal");
        animator.SetFloat("Horizontal", Mathf.Abs(horizontalMovement));
        rb.velocity= new Vector2(horizontalMovement * movementSpeed, rb.velocity.y);

        if (rb.velocity.magnitude == 0)
        {   
            walkingSound.Play();
            stoppedWalkingSound = false;
        }

        if ((isGrounded || isTopGrounded) && stoppedWalkingSound)
        {
            walkingSound.Play();
            stoppedWalkingSound = false;
        }

        if ((isGrounded || isTopGrounded) == false)
        {
            walkingSound.Stop();
            stoppedWalkingSound = true;
        }

        if (Input.GetButtonDown("Jump") && (isGrounded || isTopGrounded)) {
            pressedJump = true;
            isGrounded = false;
            isTopGrounded = false;
        }

        else if (Input.GetKeyDown(KeyCode.Space) && hasDoubleJump && !hasDoubleJumped && !isGrounded && !isTopGrounded) {
            StartDoubleJump();
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
            gravitySwiftSound.Play();
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

        if (hasSuperAttraction && Input.GetKeyDown(KeyCode.Alpha1))
        {
            isSuperAttractionActive = !isSuperAttractionActive;
            ApplySuperAttraction();
        }

        // Invulnerability timer after taking damage
        if (isInvulnerable)
        {
            invulnerabilityTimer -= Time.deltaTime;
            if (invulnerabilityTimer <= 0)
            {
                isInvulnerable = false;
            }
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
    private void StartDoubleJump() {
        rb.gravityScale = 0;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(0, gravitySense * jumpForce), ForceMode2D.Impulse);
        hasDoubleJumped = true;
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
            hasDoubleJumped = false;
        }
        if (collision.gameObject.tag == "TopGround")
        {
            isTopGrounded= true;
            hasDoubleJumped = false;
        }
    }

    // POWER UPS
    public void ObtainSuperSpeed()
    {
        hasSuperSpeed = true;
        Debug.Log("Super speed obtained");
    }
    public void ObtainSuperAttraction()
    {
        hasSuperAttraction = true;
        Debug.Log("Super attraction obtained");
    }
    public void ObtainDoubleJump()
    {
        hasDoubleJump = true;
        Debug.Log("Double jump obtained");
    }
    public void ApplySuperAttraction()
    {
        GravitySwitch[] gravitySwitches = FindObjectsOfType<GravitySwitch>();
        foreach (GravitySwitch gravitySwitch in gravitySwitches)
        {
            gravitySwitch.ModifyForceDirection(isSuperAttractionActive);
        }
    }

    //Health
    public void Heal(int heal)
    {
        currentHealth += heal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log("Player healed. Current health: " + currentHealth);
    }
    public void TakeDamage(int damage)
    {
        if (!isInvulnerable)
        {
            currentHealth -= damage;
            isInvulnerable = true;
            invulnerabilityTimer = invulnerabilityDuration;
            Debug.Log("Player took damage. Current health: " + currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }     
        }
    }
    public void Die()
    {
        Debug.Log("Player died");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
