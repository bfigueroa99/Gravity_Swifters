using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicSpawner : MonoBehaviour
{
    public GameObject relic;
    private float xOffset = 1.2f;
    private float yOffset = -1.2f;
    public void SpawnRelic()
    {
        float xValue = transform.position.x + xOffset;
        float yValue = transform.position.y + yOffset;
        Vector3 newPosition = new Vector3(xValue, yValue, transform.position.z);
        GameObject newRelic = Instantiate(relic, newPosition, Quaternion.identity);
        newRelic.name = "Relic";
        newRelic.tag = "Relic";
        newRelic.layer = 12;

        ItemPickup itemPickup = newRelic.AddComponent<ItemPickup>();
        itemPickup.player = GameObject.Find("Player");
    }
}