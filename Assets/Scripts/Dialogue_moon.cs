using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class dialogueMoon : MonoBehaviour
{
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
    public static bool pressedDialogue;
    private Rocket Rocket;
    public ItemSpawner itemSpawner;

    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject Boss;

    private bool hasPlayedDialogue; 

    void Start()
    {
        hasPlayedDialogue = false; 
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && !hasPlayedDialogue)
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
            if (SceneManager.GetActiveScene().name == "Luna")
            {
                GetComponent<Animator>().SetTrigger("desaparecer");
            }
            else if (SceneManager.GetActiveScene().name == "Marte")
            {
                GetComponent<Animator>().SetTrigger("Rotar");
            }

            hasPlayedDialogue = true; // Establecer la bandera para que no se reproduzca más de una vez
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

    private void Desaparecer()
    {
        Boss.SetActive(true);
        itemSpawner.StartSpawning();
        gameObject.SetActive(false);
    }

    private void ActivarRocket()
    {
        gameObject.SetActive(false);
        Rocket.StartMovingPlatform();
    }
}
