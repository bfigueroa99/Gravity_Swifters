using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakFloor : MonoBehaviour
{
    public GameObject player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Box") && player.GetComponent<PlayerController>().isSuperAttractionActive)
        {
            gameObject.SetActive(false);
        }
    }
}