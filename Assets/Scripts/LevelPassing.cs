using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectible : MonoBehaviour
{
    [SerializeField] private string targetSceneName; 

    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(targetSceneName);
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
