using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource walkingSound, jumpingSound, invertedGravitySound, treeSound, healSound, UISound, 
    damageSound, spikeSound, relicOpenSound, relicCloseSound;

    public AudioClip[] walkingSounds; 

    private int currentWalkingSoundIndex = 0; 

    void Start() {
        walkingSound.clip = walkingSounds[currentWalkingSoundIndex];
    }

    void Update()
    {   
        bool isPlayerMoving = PlayerController.isPlayerMoving;
        bool playerJumped = PlayerController.playerJumped;

        bool isGrounded = PlayerController.isGrounded;
        bool isTopGrounded = PlayerController.isTopGrounded;  
        bool changedGravity = PlayerController.changedGravity;  

        bool interactedWithTree = EnvironmentInteraction.interactedWithTree;    
        bool playerHealed = PlayerController.playerHealed;
        bool pressedDialogue = dialogue.pressedDialogue;
        bool tookDamage = PlayerController.tookDamage;
        bool touchedSpike = PlayerController.touchedSpike;
        bool pickedUpRelic = UIController.pickedUpRelic;
        bool closedRelicText = UIController.closedRelicText;
        Debug.Log(pickedUpRelic);

        // Audio for walking
        if (isPlayerMoving && (isTopGrounded || isGrounded))
        {
            if (!walkingSound.isPlaying)
            {   
                walkingSound.Play();

                currentWalkingSoundIndex = Random.Range(0, walkingSounds.Length);
                walkingSound.clip = walkingSounds[currentWalkingSoundIndex];
            }

        }

        // Audio for jumping
        if (playerJumped)
        {   
            walkingSound.Stop();

            if (!jumpingSound.isPlaying)
            {   
                jumpingSound.Play();
            }
        }

        // Audio for inverted gravity
        if (changedGravity)
        {   
            walkingSound.Stop();   
            invertedGravitySound.Play();
        }

        // Audio for tree interaction
        if (interactedWithTree)
        {
            if (!treeSound.isPlaying)
            {
                treeSound.Play();
            }
        }

        // Audio for player healing
        if (playerHealed)
        {
            if (!healSound.isPlaying)
            {
                healSound.Play();
            }
        }

        // Audio for pressing E on UI texts
        /*Debug.Log(pressedDialogue);
        if (pressedDialogue)
        {
            walkingSound.Stop();
            if (!UISound.isPlaying)
            {
                UISound.Play();
            }
        }*/

        // Audio for taking damage
        if (tookDamage)
        {   
            if (!damageSound.isPlaying)
            {
                damageSound.Play();
            }

            if (touchedSpike)
            {
                spikeSound.Play();
            }
        }

        if (pickedUpRelic)
        {   
            if (!relicOpenSound.isPlaying)
            {
                relicOpenSound.Play();
            }

        }

        if (closedRelicText)
        {
            if (!relicCloseSound.isPlaying)
            {
                relicCloseSound.Play();
            }
        }

        
    }
}
