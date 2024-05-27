using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovilePlatform : MonoBehaviour
{
    [SerializeField] private Transform[] movementPoints;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float delayAtPoint = 0f; 

    private int nextPlatformIndex = 1;
    private bool isMovingForward = true;

    private List<Rigidbody2D> playerContacts = new List<Rigidbody2D>();
    private Vector2 lastPlatformPosition;
    private bool isWaiting = false;

    private void Start()
    {
        lastPlatformPosition = transform.position;
        StartCoroutine(MovePlatform());
    }

    private IEnumerator MovePlatform()
    {
        while (true)
        {
            if (!isWaiting)
            {
                if (isMovingForward && nextPlatformIndex + 1 >= movementPoints.Length)
                {
                    isMovingForward = false;
                }
                else if (!isMovingForward && nextPlatformIndex <= 0)
                {
                    isMovingForward = true;
                }

                if (Vector2.Distance(transform.position, movementPoints[nextPlatformIndex].position) < 0.1f)
                {
                    nextPlatformIndex = isMovingForward ? nextPlatformIndex + 1 : nextPlatformIndex - 1;
                    isWaiting = true;
                    yield return new WaitForSeconds(delayAtPoint);
                    isWaiting = false;
                }

                transform.position = Vector2.MoveTowards(transform.position, movementPoints[nextPlatformIndex].position, movementSpeed * Time.deltaTime);

                Vector2 platformMovement = (Vector2)transform.position - lastPlatformPosition;

                foreach (var player in playerContacts)
                {
                    player.position += platformMovement;
                }

                lastPlatformPosition = transform.position;
            }

            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRB = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRB != null && !playerContacts.Contains(playerRB))
            {
                playerContacts.Add(playerRB);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRB = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRB != null && playerContacts.Contains(playerRB))
            {
                playerContacts.Remove(playerRB);
            }
        }
    }
}
