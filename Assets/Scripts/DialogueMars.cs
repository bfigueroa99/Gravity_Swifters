using System.Collections;
using UnityEngine;
using TMPro;

public class dialogueMars : MonoBehaviour
{
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
    public static bool pressedDialogue;

    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Rocket rocket; 

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
            GetComponent<Animator>().SetTrigger("Rotar");
            ActivarRocket(); // Activar el rocket cuando termine el diálogo
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

    private void ActivarRocket()
    {
        if (rocket != null)
        {
            gameObject.SetActive(false);
            rocket.StartMovingPlatform();
        }
        else
        {
            Debug.LogError("Rocket reference is not assigned in the Inspector.");
        }
    }
}
