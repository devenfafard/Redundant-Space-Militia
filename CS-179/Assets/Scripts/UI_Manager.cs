using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : Observer
{
    [SerializeField] private GameObject mainMenuPanel         = null;
    [SerializeField] private GameObject creditsPanel          = null;
    [SerializeField] private GameObject pausePanel            = null;
    [SerializeField] private GameObject loadingPanel          = null;
    [SerializeField] private GameObject gameOverPanel         = null;
    [SerializeField] private Canvas mainMenuContainerCanvas   = null;

    private CanvasGroup mainMenuPanelGroup                    = null;
    private CanvasGroup creditsPanelGroup                     = null;
    private CanvasGroup loadingPanelGroup                     = null;
    private CanvasGroup pausePanelGroup                       = null;
    private CanvasGroup gameOverPanelGroup                    = null;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
        mainMenuContainerCanvas = this.gameObject.GetComponent<Canvas>();
    }

    private void Start()
    {
        // Initialize main menu object.
        if (mainMenuPanel != null)
        {
            mainMenuPanelGroup = mainMenuPanel.GetComponent<CanvasGroup>();
            TurnOnPanel(mainMenuPanelGroup, mainMenuContainerCanvas);
        }

        // Intialize credits panel object.
        if (creditsPanel != null)
        {
            creditsPanelGroup  = creditsPanel.GetComponent<CanvasGroup>();
            TurnOffPanel(creditsPanelGroup, mainMenuContainerCanvas);
        }

        // Initialize pause menu object.
        if(pausePanel != null)
        {
            pausePanelGroup  = pausePanel.GetComponent<CanvasGroup>();
            TurnOffPanel(pausePanelGroup, mainMenuContainerCanvas);
        }

        // Initialize loading menu object.
        if(loadingPanel != null)
        {
            loadingPanelGroup = loadingPanel.GetComponent<CanvasGroup>();
            TurnOffPanel(loadingPanelGroup, mainMenuContainerCanvas);
        }

        // Initialize loading menu object.
        if (gameOverPanel != null)
        {
            gameOverPanelGroup = gameOverPanel.GetComponent<CanvasGroup>();
            TurnOffPanel(gameOverPanelGroup, mainMenuContainerCanvas);
        }
    }

    private void Update()
    {        
        if(Input.GetKeyDown(KeyCode.BackQuote) && this.GetCurrentScene() == 1)
        {
            if(pausePanelGroup.alpha == 0)
            {
                LockMouse(false);
                TurnOnPanel(pausePanelGroup, mainMenuContainerCanvas);
            }
            else
            {
                OnResumeClicked();
            }
        }
    }
    
    #region [OBJECT AGNOSTIC FUNCTIONS]
    public override void OnNotify(NotificationType type)
    {
        Debug.Log("Player died!");
        if(type == NotificationType.PLAYER_DEAD)
        {
            LockMouse(false);
            TurnOnPanel(gameOverPanelGroup, mainMenuContainerCanvas);
        }
    }

    private void TurnOnPanel(CanvasGroup panelGroup, Canvas panelCanvas)
    {
        panelGroup.alpha = 1;
        panelGroup.interactable = true;
        panelGroup.blocksRaycasts = true;
        panelCanvas.sortingOrder = 15;
    }

    private void TurnOffPanel(CanvasGroup panelGroup, Canvas panelCanvas)
    {
        panelGroup.alpha = 0;
        panelGroup.interactable = false;
        panelGroup.blocksRaycasts = false;
        panelCanvas.sortingOrder = 0;
    }

    private void LockMouse(bool mouseState)
    {
        if (mouseState == true) // locked
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }
        else // unlocked
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }

    private int GetCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.buildIndex;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 1)
        {
            TurnOffPanel(loadingPanelGroup, mainMenuContainerCanvas);
            TurnOffPanel(gameOverPanelGroup, mainMenuContainerCanvas);

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if(player != null)
            {
                HealthScript healthManager = player.GetComponentInChildren<HealthScript>();
                healthManager.AddObserver(this);
            }
        }
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
    #endregion

    #region [MAIN MENU FUNCTIONS]
    public void OnPlayClicked()
    {
        TurnOnPanel(loadingPanelGroup, mainMenuContainerCanvas);
        TurnOffPanel(mainMenuPanelGroup, mainMenuContainerCanvas);
        TurnOffPanel(gameOverPanelGroup, mainMenuContainerCanvas);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void OnCreditsClicked()
    {
        TurnOnPanel(creditsPanelGroup, mainMenuContainerCanvas);
        TurnOffPanel(mainMenuPanelGroup, mainMenuContainerCanvas);
    }

    public void OnBackClicked()
    {
        TurnOnPanel(mainMenuPanelGroup, mainMenuContainerCanvas);
        TurnOffPanel(creditsPanelGroup, mainMenuContainerCanvas);
    }
    #endregion

    #region [PAUSE MENU FUNCTIONS]
    public void OnResumeClicked()
    {
        LockMouse(true);
        TurnOffPanel(pausePanelGroup, mainMenuContainerCanvas);
    }
    #endregion
}