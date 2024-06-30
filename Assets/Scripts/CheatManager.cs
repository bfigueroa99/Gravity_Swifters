using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheatManager : MonoBehaviour
{
    public static CheatManager Instance;

    public Toggle superJumpToggle;
    public Toggle superSpeedToggle;
    public Toggle superAttractionToggle;
    public TMP_Dropdown levelDropdown;
    public GameObject panel;
    private bool isPanelVisible = true;
    private string[] sceneNames = { "Tierra", "Marte", "Mercurio", "Venus", "Jupiter", "Saturno",
                                    "Urano", "Neptuno", "Luna" };
    public GameObject player;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializeCheatManager();
    }

    void InitializeCheatManager()
    {
        panel.SetActive(false);
        levelDropdown.ClearOptions();
        foreach (string levelName in sceneNames)
        {
            levelDropdown.options.Add(new TMP_Dropdown.OptionData(levelName));
        }

        levelDropdown.onValueChanged.AddListener(OnLevelSelected);
        superJumpToggle.onValueChanged.AddListener(OnSuperJumpToggled);
        superSpeedToggle.onValueChanged.AddListener(OnSuperSpeedToggled);
        superAttractionToggle.onValueChanged.AddListener(OnSuperAttractionToggled);

        superJumpToggle.isOn = false;
        superSpeedToggle.isOn = false;
        superAttractionToggle.isOn = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ToggleCheatPanel();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void ToggleCheatPanel()
    {
        isPanelVisible = !isPanelVisible;
        panel.SetActive(isPanelVisible);
    }

    void OnLevelSelected(int index)
    {
        string selectedLevel = levelDropdown.options[index].text;
        SceneManager.LoadScene(selectedLevel);
    }

    void OnSuperJumpToggled(bool isOn)
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0)) // If the scene is not Menu
        {
           player.GetComponent<PlayerController>().hasDoubleJump = isOn; 
        }
    }

    void OnSuperSpeedToggled(bool isOn)
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0)) // If the scene is not Menu
        {
            player.GetComponent<PlayerController>().hasSuperSpeed = isOn;
        }
    }

    void OnSuperAttractionToggled(bool isOn)
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0)) // If the scene is not Menu
        {
            player.GetComponent<PlayerController>().hasSuperAttraction = isOn;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            OnSuperJumpToggled(superJumpToggle.isOn);
            OnSuperSpeedToggled(superSpeedToggle.isOn);
            OnSuperAttractionToggled(superAttractionToggle.isOn);
        }
    }
}
