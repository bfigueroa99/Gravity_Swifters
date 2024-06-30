using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float delayAtPoint = 0f;
    [SerializeField] private GameObject objectToDeactivate;

    private bool movingToPointB = true;
    private List<Rigidbody2D> playerContacts = new List<Rigidbody2D>();
    private Vector2 lastPlatformPosition;
    private bool isWaiting = false;

    private Coroutine movePlatformCoroutine;

    private void Start()
    {
        lastPlatformPosition = transform.position;
    }

    public void StartMovingPlatform()
    {
        if (movePlatformCoroutine == null)
        {
            movePlatformCoroutine = StartCoroutine(MovePlatform());
        }
    }

    private IEnumerator MovePlatform()
    {
        while (true)
        {
            if (!isWaiting)
            {
                Transform targetPoint = movingToPointB ? pointB : pointA;

                if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
                {
                    isWaiting = true;
                    yield return new WaitForSeconds(delayAtPoint);
                    isWaiting = false;

                    // Desaparece y desactiva el objeto cuando alcanza el punto
                    if (targetPoint == pointA || targetPoint == pointB)
                    {
                        gameObject.SetActive(false);
                        if (objectToDeactivate != null)
                        {
                            objectToDeactivate.SetActive(false);
                        }
                        yield break; 
                    }

                    movingToPointB = !movingToPointB;
                }

                transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, movementSpeed * Time.deltaTime);

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

}
