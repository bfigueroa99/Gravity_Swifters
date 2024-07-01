using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 

public class FinalDialogue : MonoBehaviour
{
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
    public static bool pressedDialogue;

    [SerializeField, TextArea(4,6)] private string[] dialogueLines;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private string nextSceneName;  

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            pressedDialogue = true;
            StartCoroutine(ResetPressedDialogue());

            if (!didDialogueStart)
            {
                StartDialogue();           
            }
            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowDialogue());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowDialogue());
        }
        else
        {
            dialoguePanel.SetActive(false);
            didDialogueStart = false;
            Time.timeScale = 1f;
            StartCoroutine(WaitAndLoadScene());  
        }
    }

    private IEnumerator ShowDialogue()
    {
        dialogueText.text = string.Empty;

        foreach (char letter in dialogueLines[lineIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }

    private IEnumerator ResetPressedDialogue()
    {
        yield return new WaitForSeconds(0.05f); 
        pressedDialogue = false;
    }

    private IEnumerator WaitAndLoadScene()  
    {
        yield return new WaitForSeconds(3f);  
        SceneManager.LoadScene(nextSceneName);  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
