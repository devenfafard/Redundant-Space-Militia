using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Observer
{
    [SerializeField] private Canvas containerCanvas = null;
    [SerializeField] private GameObject mainMenuPanel = null;
    [SerializeField] private GameObject creditsPanel = null;
    [SerializeField] private GameObject pausePanel = null;
    [SerializeField] private GameObject loadingPanel = null;
    [SerializeField] private GameObject gameOverPanel = null;
    [SerializeField] private GameObject winPanel = null;
    
    private CanvasGroup mainMenuPanelGroup = null;
    private CanvasGroup creditsPanelGroup = null;
    private CanvasGroup loadingPanelGroup = null;
    private CanvasGroup pausePanelGroup = null;
    private CanvasGroup gameOverPanelGroup = null;
    private CanvasGroup winPanelGroup = null;

    private void Awake() // Make the UI system persist between scenes + subscribe UIManager to GameManager updates.
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(containerCanvas);
        GameManager.Instance.AddObserver(this);
    }

    void Start() // Init panels.
    {
        // Initialize main menu object.
        if (mainMenuPanel != null)
        {
            mainMenuPanelGroup = mainMenuPanel.GetComponent<CanvasGroup>();
            TurnOnPanel(mainMenuPanelGroup, containerCanvas);
        }

        // Intialize credits menu object.
        if (creditsPanel != null)
        {
            creditsPanelGroup = creditsPanel.GetComponent<CanvasGroup>();
            TurnOffPanel(creditsPanelGroup, containerCanvas);
        }

        // Initialize pause menu object.
        if (pausePanel != null)
        {
            pausePanelGroup = pausePanel.GetComponent<CanvasGroup>();
            TurnOffPanel(pausePanelGroup, containerCanvas);
        }

        // Initialize loading menu object.
        if (loadingPanel != null)
        {
            loadingPanelGroup = loadingPanel.GetComponent<CanvasGroup>();
            TurnOffPanel(loadingPanelGroup, containerCanvas);
        }

        // Initialize game over menu object.
        if (gameOverPanel != null)
        {
            gameOverPanelGroup = gameOverPanel.GetComponent<CanvasGroup>();
            TurnOffPanel(gameOverPanelGroup, containerCanvas);
        }

        // Initialize win menu object.
        if (winPanel != null)
        {
            winPanelGroup = winPanel.GetComponent<CanvasGroup>();
            TurnOffPanel(winPanelGroup, containerCanvas);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote) && !(GameManager.Instance.GetBuildIndex() == 0))
        {
            if (pausePanelGroup.alpha == 0)
            {
                GameManager.Instance.LockMouse(false);
                TurnOnPanel(pausePanelGroup, containerCanvas);
            }
            else
            {
                OnResumeClicked();
            }
        }
    }

    #region [BUTTON FUNCTIONS]
    public void OnStartButtonClicked() // Display the main menu on top + tell GameManager to load the first level.
    {
        TurnOnPanel(loadingPanelGroup, containerCanvas);
        TurnOffPanel(mainMenuPanelGroup, containerCanvas);
        TurnOffPanel(gameOverPanelGroup, containerCanvas);
        GameManager.Instance.LoadScene(1);
    }

    public void OnCreditsButtonClicked() // Display the credits on top.
    {
        TurnOnPanel(creditsPanelGroup, containerCanvas);
        TurnOffPanel(mainMenuPanelGroup, containerCanvas);
    }

    public void OnBackButtonClicked()  // Close the credits + bring up the main menu.
    {
        TurnOnPanel(mainMenuPanelGroup, containerCanvas);
        TurnOffPanel(creditsPanelGroup, containerCanvas);
    }

    public void OnResumeClicked() // Unlock the mouse + close the pause panel.
    {
        GameManager.Instance.LockMouse(true);
        TurnOffPanel(pausePanelGroup, containerCanvas);
    }

    public void OnQuitButtonClicked() // Tell GameManager to kill the application.
    {
        GameManager.Instance.QuitGame();
    }
    #endregion

    #region [OBJECT AGNOSTIC FUNCTIONS]
    public override void OnNotify(NotificationType type)
    {
        switch(type)
        {
            case NotificationType.UI_LEVEL1_START:
                TurnOffPanel(loadingPanelGroup, containerCanvas);
                TurnOffPanel(gameOverPanelGroup, containerCanvas);
                TurnOffPanel(winPanelGroup, containerCanvas);
                // TODO : Display level 1 UI notification
                break;
            case NotificationType.UI_LEVEL2_START:
                TurnOffPanel(loadingPanelGroup, containerCanvas);
                TurnOffPanel(gameOverPanelGroup, containerCanvas);
                TurnOffPanel(winPanelGroup, containerCanvas);
                // TODO : Display level 2 UI notification
                break;
            case NotificationType.GLOBAL_GAME_OVER:
                GameManager.Instance.LockMouse(false);
                TurnOnPanel(gameOverPanelGroup, containerCanvas);
                break;
        }
    }
   
    private void TurnOnPanel(CanvasGroup panelGroup, Canvas panelCanvas)
    {
        panelGroup.alpha = 1;
        panelGroup.interactable = true;
        panelGroup.blocksRaycasts = true;
        panelCanvas.sortingOrder = 10;
    }

    private void TurnOffPanel(CanvasGroup panelGroup, Canvas panelCanvas)
    {
        panelGroup.alpha = 0;
        panelGroup.interactable = false;
        panelGroup.blocksRaycasts = false;
        panelCanvas.sortingOrder = 0;
    }
    #endregion
}