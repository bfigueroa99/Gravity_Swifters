using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public Camera startCamera;
    public Camera mainCamera;
    public GameObject mainCameraChildSprite;

    public float overviewDuration = 3.0f;
    public float zoomSpeed = 2.0f; 

    private float timer = 0.0f;
    private bool isTransitioning = false;

    void Start()
    {
        mainCamera.enabled = false; 
        startCamera.enabled = true;
        mainCameraChildSprite.SetActive(false);

    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= overviewDuration && !isTransitioning)
        {
            isTransitioning = true;
        }

        if (isTransitioning)
        {
            startCamera.orthographicSize = Mathf.Lerp(startCamera.orthographicSize, mainCamera.orthographicSize, Time.deltaTime * zoomSpeed);
            startCamera.transform.position = Vector3.Lerp(startCamera.transform.position, mainCamera.transform.position, Time.deltaTime * zoomSpeed);

            if (Vector3.Distance(startCamera.transform.position, mainCamera.transform.position) < 0.1f && Mathf.Abs(startCamera.orthographicSize - mainCamera.orthographicSize) < 0.1f)
            {
                startCamera.enabled = false;
                mainCamera.enabled = true;
                mainCameraChildSprite.SetActive(true);
                isTransitioning = false;
            }
        }
    }
}
