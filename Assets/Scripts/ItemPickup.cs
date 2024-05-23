using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public GameObject player;
    private bool pickupAllowed;

    // Update is called once per frame
    void Update()
    {
        if (pickupAllowed && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            pickupAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            pickupAllowed = false;
        }
    }

    private void PickUp()
    {
        if(gameObject.tag.Equals("Power")){
            player.GetComponent<PlayerPowers>().AddItem(gameObject);
            Destroy(gameObject);
        }
        else if(gameObject.tag.Equals("Relic")){
            Destroy(gameObject);
            Debug.Log("Relic picked up");
        }
    }
}
