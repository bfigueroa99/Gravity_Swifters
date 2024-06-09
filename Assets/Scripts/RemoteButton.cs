using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteButton : MonoBehaviour
{
    public GameObject gate;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            gate.SetActive(false);
        }
        else {
            gate.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            gate.SetActive(true);
        }
    }
}
