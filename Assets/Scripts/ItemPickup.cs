using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public GameObject player;
    private bool pickupAllowed;
    private UIController uiController;

    void Start()
    {
        uiController = FindObjectOfType<UIController>();
        if (uiController == null)
        {
            Debug.LogError("UIController not found in the scene!");
        }
    }

    void Update()
    {
        if (pickupAllowed && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pickupAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pickupAllowed = false;
        }
    }

    private void PickUp()
    {
        if (gameObject.CompareTag("Power"))
        {
            player.GetComponent<PlayerPowers>().AddItem(gameObject);
            Destroy(gameObject);
            Debug.Log("Power picked up and destroyed.");
        }
        else if (gameObject.CompareTag("Relic"))
        {
            Debug.Log("Relic picked up");
            if (uiController != null)
            {
                uiController.ShowImage();
            }
            Destroy(gameObject);
        }
    }
}
