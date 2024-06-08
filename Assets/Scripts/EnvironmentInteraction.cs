using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentInteraction : MonoBehaviour
{
    private bool interactionAllowed;
    private bool alreadyInteracted;
    public static bool interactedWithTree;

    void Update()
    {
        interactedWithTree = false;

        if (interactionAllowed && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            interactionAllowed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            interactionAllowed = false;
        }
    }
    private void Interact()
    {
        if(gameObject.name.Equals("Tree1") && !alreadyInteracted){
            GetComponent<RelicSpawner>().SpawnRelic();
            alreadyInteracted = true;
            interactedWithTree = true;
        }
    }
}
