using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontalMovement;
    public float movementSpeed= 2f; 
    public float jumpForce= 10f;
    private bool isGrounded= false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()

    {   // Movimiento en eje X y salto
        float horizontalMovement= Input.GetAxis("Horizontal");
        rb.velocity= new Vector2(horizontalMovement * movementSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("Salto activado");
            rb.velocity= new Vector2(rb.velocity.x, jumpForce);
            isGrounded= false;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {   
            Debug.Log("Tocando suelo");
            isGrounded= true;
        }
    }
}
