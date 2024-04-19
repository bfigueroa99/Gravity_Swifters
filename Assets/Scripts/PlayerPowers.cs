using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowers : MonoBehaviour
{
    public GameObject player;
    private List<String> grabbedItems = new List<String>();

    public void AddItem(GameObject item)
    {
        grabbedItems.Add(item.name);
        if (item.name == "SuperSpeed")
        {
            player.GetComponent<PlayerController>().ObtainSuperSpeed();
        }
    }

}
