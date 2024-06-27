using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public float spawnInterval = 2f;
    private Camera mainCamera;
    private bool shouldSpawn = false;
    private GameObject lastSpawnedItem;

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
        Vector3 spawnPosition;
        bool isValidPosition = false;

        // Buscar una posición válida para spawnear
        do
        {
            Vector3 screenBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
            Vector3 screenTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

            float randomX = Random.Range(screenBottomLeft.x, screenTopRight.x);
            float randomY = Random.Range(screenBottomLeft.y, screenTopRight.y);
            spawnPosition = new Vector2(randomX, randomY);

            // Comprobar si la posición es válida (no colisiona con el tilemap "Ground")
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

        // Destruir el último objeto spawneado
        if (lastSpawnedItem != null)
        {
            Destroy(lastSpawnedItem);
        }

        // Instanciar el nuevo prefab y guardar la referencia
        lastSpawnedItem = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
    }

    // Método para activar el spawn
    public void StartSpawning()
    {
        shouldSpawn = true;
    }
}
