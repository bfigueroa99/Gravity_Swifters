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
    private bool isTopGrounded= false;
    
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

        if (Input.GetButtonDown("Jump") && isTopGrounded)
        {
            Debug.Log("Salto activado");
            rb.velocity= new Vector2(rb.velocity.x, -jumpForce);
            isTopGrounded= false;

        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Cambio de gravedad");
            rb.gravityScale *= -1;
            CharacterRotation();
        }
    }

    void CharacterRotation(){
        if (isTopGrounded == false)
        {
            transform.eulerAngles= new Vector3(0, 0, 180);
        }
        else 
        {
            transform.eulerAngles= new Vector3(0, 0, 0);
        }

        isTopGrounded= !isTopGrounded;
    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {   
            Debug.Log("Tocando suelo");
            isGrounded= true;
        }
        if (collision.gameObject.tag == "TopGround")
        {
            Debug.Log("Tocando techo");
            isTopGrounded= true;
        }
    }
}
