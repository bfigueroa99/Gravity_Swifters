using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class OrbAnimation : MonoBehaviour
{
    Transform transform;
    float distance;


    void Start()
    {
        transform = GetComponent<Transform>();
        distance = -0.004f;
        StartCoroutine(ChangeDirection());
    }

    void Update()
    {
        OrbLevitation();
    }

    void OrbLevitation()
    {   
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y + distance);
        transform.position = currentPosition;

    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            distance *= -1; 
        }
    }
}