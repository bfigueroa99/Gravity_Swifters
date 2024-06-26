using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public float spawnInterval = 2f;
    private Camera mainCamera;
    private bool shouldSpawn = false;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (shouldSpawn)
        {
            CancelInvoke("SpawnItem");
            InvokeRepeating("SpawnItem", 0f, spawnInterval);
            shouldSpawn = false; 
        }
    }

    private void SpawnItem()
    {
       
        Vector3 screenBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 screenTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        
        float randomX = Random.Range(screenBottomLeft.x, screenTopRight.x);
        float randomY = Random.Range(screenBottomLeft.y, screenTopRight.y);
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        
        Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
    }

    
    public void StartSpawning()
    {
        shouldSpawn = true;
    }
}
