using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public float spawnInterval = 2f;
    private bool shouldSpawn = false;
    private GameObject lastSpawnedItem;


    private Vector2 topLeft = new Vector2(143.19f, 51.6f);
    private Vector2 bottomLeft = new Vector2(143.19f, 17.54f);
    private Vector2 bottomRight = new Vector2(216.6f, 17.54f);
    private Vector2 topRight = new Vector2(216.6f, 51.6f);

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
        Vector3 spawnPosition;
        bool isValidPosition = false;


        do
        {
            float randomX = Random.Range(topLeft.x, topRight.x);
            float randomY = Random.Range(bottomLeft.y, topLeft.y);
            spawnPosition = new Vector2(randomX, randomY);


            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, 0.5f);
            isValidPosition = true;
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Ground"))
                {
                    isValidPosition = false;
                    break;
                }
            }

        } while (!isValidPosition);


        if (lastSpawnedItem != null)
        {
            Destroy(lastSpawnedItem);
        }


        lastSpawnedItem = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
    }


    public void StartSpawning()
    {
        shouldSpawn = true;
    }
}
