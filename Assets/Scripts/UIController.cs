using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour
{
    public Image displayImage;
    private bool canHide;
    public static bool pickedUpRelic;
    public static bool closedRelicText;

    public void ShowImage()
    {   
        if (displayImage != null)
        {
            displayImage.gameObject.SetActive(true);
            pickedUpRelic = true;

            Debug.Log("Image displayed");
            StartCoroutine(HideImageAfterDelay());
        }
        else
        {
            Debug.LogWarning("Display Image is not assigned!");
        }
    }

    private IEnumerator HideImageAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        if (displayImage != null)
        {
            canHide = true;
            Debug.Log("Image can be hidden");
        }
    }

    void Update()
    {    
        pickedUpRelic = false;
        closedRelicText = false;

        if (displayImage != null && canHide == true && Input.GetKeyDown(KeyCode.E))
        {
            closedRelicText = true;

            displayImage.gameObject.SetActive(false);
            Debug.Log("Image hidden by pressing E");

            canHide = false;
        }
    }
}
