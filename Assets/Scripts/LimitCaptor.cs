using UnityEngine;
using System.Collections;

public class PlatformTrigger : MonoBehaviour
{
    public ItemSpawner itemSpawner;
    private BoxCollider2D platformCollider;
    public BoxCollider2D otherPlatformCollider;
    [SerializeField] private GameObject Boss;
    [SerializeField] private GameObject BarradeVida;

    private void Start()
    {
        platformCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {  
            Debug.Log("Player touched the platform.");
            StartCoroutine(DisableIsTriggerAfterDelay(1f)); 
        }
    }

    private IEnumerator DisableIsTriggerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        itemSpawner.StartSpawning();
        Boss.SetActive(true); 
        platformCollider.isTrigger = false;
        otherPlatformCollider.isTrigger = false;
        Debug.Log("Platform trigger option disabled.");
    }
}
