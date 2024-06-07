using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowers : MonoBehaviour
{
    public GameObject player;
    private List<String> grabbedItems = new List<String>();
    [SerializeField] private GameObject superSpeed;
    [SerializeField] private GameObject superAttraction;
    [SerializeField] private GameObject doubleJump;

    private void Start()
    {
        superAttraction.SetActive(false);
        superSpeed.SetActive(false);
        doubleJump.SetActive(false);
    }

    public void AddItem(GameObject item)
    {
        grabbedItems.Add(item.name);
        if (item.name == "SuperSpeed")
        {
            player.GetComponent<PlayerController>().ObtainSuperSpeed();
            superSpeed.SetActive(true);
        }
        if (item.name == "SuperAttraction")
        {
            player.GetComponent<PlayerController>().ObtainSuperAttraction();
            superAttraction.SetActive(true);
        }
        if (item.name == "DoubleJump")
        {
            player.GetComponent<PlayerController>().ObtainDoubleJump();
            doubleJump.SetActive(true);
        }
    }

}
