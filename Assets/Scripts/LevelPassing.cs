using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPassing : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float movementSpeed;
    [SerializeField] private GameObject objectToDeactivate;
    [SerializeField] private string targetSceneName;

    private bool movingToPointB = false;
    private List<Rigidbody2D> playerContacts = new List<Rigidbody2D>();
    private Vector2 lastPlatformPosition;
    private bool playerInRange = false;
    private Coroutine movePlatformCoroutine;

    private void Start()
    {
        lastPlatformPosition = transform.position;
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            StartMovingPlatform();
        }
    }

    public void StartMovingPlatform()
    {
        if (movePlatformCoroutine == null)
        {
            if (objectToDeactivate != null)
            {
                objectToDeactivate.SetActive(false);
            }
            movingToPointB = true;
            movePlatformCoroutine = StartCoroutine(MovePlatform());
        }
    }

    private IEnumerator MovePlatform()
    {
        while (movingToPointB)
        {
            Transform targetPoint = pointB;

            if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
            {
                // Cambiar de escena cuando se alcance el punto B
                SceneManager.LoadScene(targetSceneName);
                yield break; // Salir de la corrutina
            }

            transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, movementSpeed * Time.deltaTime);

            Vector2 platformMovement = (Vector2)transform.position - lastPlatformPosition;

            foreach (var player in playerContacts)
            {
                player.position += platformMovement;
            }

            lastPlatformPosition = transform.position;
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
