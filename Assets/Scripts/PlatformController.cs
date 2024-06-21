using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Transform pos1, pos2, pos3, pos4;
    Vector2 nextPos;
    public int speed;

    void Update()
    {
        if (Vector2.Distance(transform.position, pos1.position) < 0.1f)
        {
            nextPos = pos2.position;
        }
        else if (Vector2.Distance(transform.position, pos2.position) < 0.1f)
        {
            nextPos = pos3.position;
        }
        else if (Vector2.Distance(transform.position, pos3.position) < 0.1f)
        {
            nextPos = pos4.position;
        }
        else if (Vector2.Distance(transform.position, pos4.position) < 0.1f)
        {
            nextPos = pos1.position;
        }    

        transform.position = Vector2.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
