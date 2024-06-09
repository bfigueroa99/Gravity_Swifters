using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Jugar(){
        SceneManager.LoadScene(1);
    }

    public void Exit(){
        Debug.Log("Exit....");
        Application.Quit();
    }

}
