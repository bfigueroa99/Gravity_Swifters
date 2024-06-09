using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudio : MonoBehaviour
{
    public AudioSource buttonSound;
    public AudioClip buttonHover;
    public AudioClip buttonClick;

    public void hoverSound()
    {
        buttonSound.PlayOneShot(buttonHover);
    }

    public void clickSound()
    {
        buttonSound.PlayOneShot(buttonClick);
    }
}
