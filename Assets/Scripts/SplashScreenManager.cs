using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashScreenManager : MonoBehaviour
{
    public float displayTime = 5f;
    public string nextSceneName = "NextScene"; 
    private void Start()
    {
        StartCoroutine(ShowSplashScreen());
    }

    private IEnumerator ShowSplashScreen()
    {
        yield return new WaitForSeconds(displayTime);
        SceneManager.LoadScene(nextSceneName);
    }
}
