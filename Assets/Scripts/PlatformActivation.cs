using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformActivation : MonoBehaviour
{
    public GameObject[] vanishingPlatforms;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            foreach (GameObject platform in vanishingPlatforms)
            {
                platform.SetActive(false);
            }
        }
        else {
            foreach (GameObject platform in vanishingPlatforms)
            {
                platform.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            foreach (GameObject platform in vanishingPlatforms)
            {
                platform.SetActive(true);
            }
        }
    }
}
