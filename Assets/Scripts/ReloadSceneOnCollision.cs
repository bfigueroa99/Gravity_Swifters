using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadSceneOnCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.ResetInstanceVariables();
            }
            ResetPlayerStaticVariables();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void ResetPlayerStaticVariables()
    {
        PlayerController.isGrounded = true;
        PlayerController.isTopGrounded = false;
        PlayerController.isInverted = false;
        PlayerController.changedGravity = false;
        // Aqu� tambi�n puedes restablecer otras variables est�ticas si es necesario.
    }
}
