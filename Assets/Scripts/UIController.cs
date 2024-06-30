using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIController : MonoBehaviour
{
    public List<Image> displayImages;
    private int currentImageIndex = 0;
    private bool canHide;
    public static bool pickedUpRelic;
    public static bool closedRelicText;

    void Start()
    {
        foreach (Image img in displayImages)
        {
            img.gameObject.SetActive(false);
        }
    }

    public void ShowImage()
    {
        if (displayImages.Count > 0 && displayImages[currentImageIndex] != null)
        {
            displayImages[currentImageIndex].gameObject.SetActive(true);
            pickedUpRelic = true;

            Debug.Log("Image displayed");
            StartCoroutine(HideImageAfterDelay());
        }
        else
        {
            Debug.LogWarning("Display Images are not assigned or the list is empty!");
        }
    }

    private IEnumerator HideImageAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        if (displayImages.Count > 0 && displayImages[currentImageIndex] != null)
        {
            canHide = true;
            Debug.Log("Image can be hidden");
        }
    }

    void Update()
    {
        pickedUpRelic = false;
        closedRelicText = false;

        if (displayImages.Count > 0 && canHide)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                displayImages[currentImageIndex].gameObject.SetActive(false);
                Debug.Log("Image hidden by pressing Enter");

                currentImageIndex = (currentImageIndex + 1) % displayImages.Count;
                displayImages[currentImageIndex].gameObject.SetActive(true);
                Debug.Log("Next image displayed");

                canHide = false;
                StartCoroutine(HideImageAfterDelay());
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                displayImages[currentImageIndex].gameObject.SetActive(false);
                Debug.Log("Image hidden by pressing E");
                closedRelicText = true;

                canHide = false;
            }
        }
    }
}
