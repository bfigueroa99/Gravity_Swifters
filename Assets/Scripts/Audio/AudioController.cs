using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource grassWalkingSound, dirtWalkingSound, jumpingSound, invertedGravitySound, treeSound, healSound, UISound, 
    damageSound, spikeSound, relicOpenSound, relicCloseSound, introBossFight, bossFightLoop, bossDamageSound;

    public AudioClip[] walkingSounds; 

    private int currentWalkingSoundIndex = 0; 
    private bool walkingOnDirt = false;
    private bool introBoss = true;


    void Start() 
    {
        grassWalkingSound.clip = walkingSounds[currentWalkingSoundIndex];
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
        bool triggeredBossFight = Boss.triggeredBossFight;
        bool bossDied = Boss.bossDied;
        bool bossReceivedDamage = Boss.bossReceivedDamage;
        
        // Audio for inverted gravity
        if (changedGravity)
        {   
            grassWalkingSound.Stop();   
            dirtWalkingSound.Stop();
            invertedGravitySound.Play();
            walkingOnDirt = !walkingOnDirt;
        }

        // Audio for walking
        if (isPlayerMoving && (isTopGrounded || isGrounded))
        {
            if (walkingOnDirt)
            {
                if (!dirtWalkingSound.isPlaying)
                {   
                    dirtWalkingSound.Play();
                }
            }
            else
            {
                if (!grassWalkingSound.isPlaying)
                {   
                    grassWalkingSound.Play();

                    currentWalkingSoundIndex = Random.Range(0, walkingSounds.Length);
                    grassWalkingSound.clip = walkingSounds[currentWalkingSoundIndex];
                }
            }

        }

        // Audio for jumping
        if (playerJumped)
        {   
            grassWalkingSound.Stop();

            if (!jumpingSound.isPlaying)
            {   
                jumpingSound.Play();
            }
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

           /* if (touchedSpike)
            {
                spikeSound.Play();
            }*/
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

        if (bossDied)
        {   
            IEnumerator fadeOutBossLoop = FadeOut (bossFightLoop, 1f);

            StartCoroutine(fadeOutBossLoop);
            StopCoroutine(fadeOutBossLoop);
        }
        else if (bossReceivedDamage)
        {
            if (!bossDamageSound.isPlaying)
            {
                bossDamageSound.Play();
            }
        }
        else if (triggeredBossFight)
        {
            if (introBoss)
            {
                introBossFight.Play();
                introBoss = false;  
            }

            if (!bossFightLoop.isPlaying && !introBossFight.isPlaying && !bossDied)
            {
                bossFightLoop.Play();
            }
        }
        
    }

    public static IEnumerator FadeOut (AudioSource audioSource, float FadeTime) 
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}
