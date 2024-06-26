using System;
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
    public float movementSpeed = 8f; 
    public float jumpForce = 16f;
    public float doubleJumpForce = 12f;
    public float gravitySense = 1f;
    public static bool isGrounded = false; 
    public static bool isTopGrounded = false;
    public static bool isPlayerMoving;
    public static bool playerJumped;
    public static bool isInverted = false;
    public static bool changedGravity;
    public static bool playerHealed;
    public static bool tookDamage;
    public static bool touchedSpike;
    bool isDoubleTap = false;
    private float lastTapTime = 0f;
    public float doubleTapTimeThreshold = 0.5f;
    [SerializeField] GameObject[] vidas;

    // POWER UPS
    private bool hasSuperSpeed = false;
    private bool isSpeedBoosted = false;
    private float speedBoostDuration = 3f;
    private float speedBoostEndTime = 0f;
    public bool hasSuperAttraction = false;
    public bool isSuperAttractionActive = false;
    private bool hasDoubleJump = false;
    private bool doubleJumpWindow = false;

    [Header("Animation")]
    private Animator animator;
    private Vector3 originalScale;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        originalScale = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    void Update()

    {   // Movimiento en eje X
        float horizontalMovement= Input.GetAxis("Horizontal");
        animator.SetFloat("Horizontal", Mathf.Abs(horizontalMovement));
        rb.velocity= new Vector2(horizontalMovement * movementSpeed, rb.velocity.y);
        bool pressedDialogue = dialogue.pressedDialogue;
        Debug.Log(pressedDialogue);
        Debug.Log("PLAYER");

        if (pressedDialogue)
        {
            isPlayerMoving = false;
        }

        if (rb.velocity.magnitude > 0)
        {
            isPlayerMoving = true;
        }
        else
        {
            isPlayerMoving = false;
        }

        if (horizontalMovement < 0)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z); // Hacia la izquierda
        }
        else if (horizontalMovement > 0)
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z); // Hacia la derecha
        }

        // Salto
        if ((isGrounded || isTopGrounded) && !Input.GetButton("Jump"))
        {
            doubleJumpWindow = false;
        }

        playerJumped = false;

        if (Input.GetButtonDown("Jump"))
        {
            playerJumped = true;

            if (isGrounded || isTopGrounded)
            {
                Debug.Log("Salta");
                if(isInverted){
                    rb.velocity = new Vector2(rb.velocity.x, -jumpForce);
                } else {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                }
                doubleJumpWindow = !doubleJumpWindow;
                isGrounded = false;
                isTopGrounded = false;
            }

            else if ((!isGrounded || !isTopGrounded) && doubleJumpWindow && hasDoubleJump){
                rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);

                doubleJumpWindow = !doubleJumpWindow;
            }
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        changedGravity = false;

        // Cambio de gravedad
        if (Input.GetKeyDown(KeyCode.LeftShift) && (isGrounded || isTopGrounded))
        {
            Debug.Log("Cambio de gravedad");
            isInverted= !isInverted;
            changedGravity = true;
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

        //actualizar vidas
        UpdateHealthUI();
    }

    private void FixedUpdate() {
        
        animator.SetBool("onFloor",isGrounded);


        if (isDoubleTap && hasSuperSpeed && !isSpeedBoosted)
        {
            movementSpeed += 8;
            isDoubleTap = false;
            isSpeedBoosted = true;
            speedBoostEndTime = Time.time + speedBoostDuration;
            Debug.Log("Double tap");
        }
    }

    void CharacterRotation(){
        if (gravitySense == -1)
        {
            transform.eulerAngles = new Vector3(0, 180, 180);
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

        if (collision.gameObject.tag == "Spike")
        {
            touchedSpike = true;
            StartCoroutine(ResetTouchedSpike());
        }
    }

    IEnumerator ResetTouchedSpike()
    {
        yield return new WaitForSeconds(1f); 
        touchedSpike = false;
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
        playerHealed = true;
        StartCoroutine(ResetPlayerHealed());
        Debug.Log("Player healed. Current health: " + currentHealth);
    }

    IEnumerator ResetPlayerHealed()
    {
        yield return new WaitForSeconds(1f); 
        playerHealed = false;
    }


    public void TakeDamage(int damage)
    {
        if (!isInvulnerable)
        {
            currentHealth -= damage;
            isInvulnerable = true;
            invulnerabilityTimer = invulnerabilityDuration;
            tookDamage = true;
            StartCoroutine(ResetTookDamage());
            StartCoroutine(ChangeColorOnDamage());

            if (currentHealth <= 0)
            {
                Die();
            }     
        }
    }

    IEnumerator ResetTookDamage()
    {
        yield return new WaitForSeconds(0.1f); 
        tookDamage = false;
    }

    private IEnumerator ChangeColorOnDamage()
    {
        // Cambia el color a rojo
        spriteRenderer.color = Color.red;
        // Espera un corto per�odo de tiempo (0.1 segundos)
        yield return new WaitForSeconds(0.1f);
        // Vuelve al color original
        spriteRenderer.color = Color.white;
    }
    public void Die()
    {
        tookDamage = false;
        Debug.Log("Player died");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateHealthUI()
    {
        for (int i = 0; i < vidas.Length; i++)
        {
            vidas[i].SetActive(i < currentHealth);
        }
    }
}
