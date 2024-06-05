using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public float speed = 5f;
    private int direction = 1; 
    public float raycastDistance = 1f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);

        if (direction < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check that the collision object is not the player
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "TopGround")
        {
            // Flip the direction when a collision occurs
            direction *= -1;
        }
    }
}
