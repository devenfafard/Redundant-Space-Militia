﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : Subject
{
    [SerializeField] private Canvas containerCanvas = null;
    [SerializeField] private GameObject mainMenuPanel = null;
    [SerializeField] private GameObject creditsPanel = null;
    [SerializeField] private GameObject pausePanel = null;
    [SerializeField] private GameObject loadingPanel = null;
    [SerializeField] private GameObject gameOverPanel = null;
    [SerializeField] private GameObject winPanel = null;
    [SerializeField] private GameObject hudPanel = null;
    [SerializeField] private Image health_stats = null;
    [SerializeField] private Image stamina_stats = null;

    private CanvasGroup mainMenuPanelGroup = null;
    private CanvasGroup creditsPanelGroup = null;
    private CanvasGroup loadingPanelGroup = null;
    private CanvasGroup pausePanelGroup = null;
    private CanvasGroup gameOverPanelGroup = null;
    private CanvasGroup winPanelGroup = null;
    private CanvasGroup hudPanelGroup = null;

    private bool isCoroutineRunning = false;

    [SerializeField] private TextMeshProUGUI notificationBody = null;

    [SerializeField] private string introText;
    [SerializeField] private string firstCheckpointText;
    [SerializeField] private string secondCheckpointText;
    [SerializeField] private string firstBaseText;
    [SerializeField] private string secondBaseText;
    [SerializeField] private string directions;
    [SerializeField] private string firstTerminalText;
    [SerializeField] private string secondTerminalText;

    #region [MONOBEHAVIORS]
    private void Awake() // Make the UI system persist between scenes
    {
        DontDestroyOnLoad(this);
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

        // Initialize the HUD object.
        if (hudPanel != null)
        {
            hudPanelGroup = hudPanel.GetComponent<CanvasGroup>();
            TurnOffPanel(hudPanelGroup, containerCanvas);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote) && !(GameManager.Instance.GetBuildIndex() == 0))
        {
            if (pausePanelGroup.alpha == 0)
            {
                LockMouse(false);
                TurnOnPanel(pausePanelGroup, containerCanvas);
            }
            else
            {
                OnResumeClicked();
            }
        }
    }
    #endregion

    #region [BUTTON FUNCTIONS]
    public void OnStartButtonClicked() // Display the main menu on top + tell GameManager to load the first level.
    {
        TurnOnPanel(loadingPanelGroup, containerCanvas);
        TurnOffPanel(mainMenuPanelGroup, containerCanvas);
        TurnOffPanel(gameOverPanelGroup, containerCanvas);
        Notify(NotificationType.START_GAME);
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
        LockMouse(true);
        TurnOffPanel(pausePanelGroup, containerCanvas);
    }

    public void OnQuitButtonClicked() // Tell GameManager to kill the application.
    {
        Notify(NotificationType.QUIT_GAME);
    }
    #endregion

    #region [OBJECT AGNOSTIC FUNCTIONS]
    public void OnNotify(NotificationType type)
    {
        switch (type)
        {
            case NotificationType.LEVEL1_START:
                TurnOffPanel(loadingPanelGroup, containerCanvas);
                TurnOffPanel(gameOverPanelGroup, containerCanvas);
                TurnOffPanel(winPanelGroup, containerCanvas);
                TurnOnPanel(hudPanelGroup, containerCanvas);
                if (notificationBody != null)
                {
                    notificationBody.text = introText;
                    StartCoroutine(ClearTextAfterSeconds(notificationBody, 5.0f));
                }
                break;

            case NotificationType.FIRST_CHECKPOINT_DONE:
                notificationBody.text = firstCheckpointText;
                if (notificationBody != null)
                {
                    notificationBody.text = firstCheckpointText;
                    StartCoroutine(ClearTextAfterSeconds(notificationBody, 5.0f));
                }
                break;

            case NotificationType.SECOND_CHECKPOINT_DONE:
                notificationBody.text = secondCheckpointText;
                if (notificationBody != null)
                {
                    notificationBody.text = secondCheckpointText;
                    StartCoroutine(ClearTextAfterSeconds(notificationBody, 5.0f));
                }
                break;

            case NotificationType.LEVEL2_START:
                TurnOffPanel(loadingPanelGroup, containerCanvas);
                TurnOffPanel(gameOverPanelGroup, containerCanvas);
                TurnOffPanel(winPanelGroup, containerCanvas);
                // TODO : Display level 2 UI notification
                break;

            case NotificationType.POWERBASE_1_DONE:
                if (notificationBody != null)
                {
                    notificationBody.text = firstBaseText;
                    if (isCoroutineRunning == false)
                    {
                        Debug.Log("QU'EST-CE QUE LE FUCK 1");
                        StartCoroutine(ClearTextAfterSeconds(notificationBody, 5.0f));
                    }
                }
                break;

            case NotificationType.POWERBASE_2_DONE:
                if (notificationBody != null)
                {
                    notificationBody.text = secondBaseText;
                    if (isCoroutineRunning == false)
                    {
                        Debug.Log("QU'EST-CE QUE LE FUCK 2");
                        StartCoroutine(ClearTextAfterSeconds(notificationBody, 5.0f));
                    }
                }
                break;

            case NotificationType.TERMINAL_1_DONE:
                if (notificationBody != null)
                {
                    notificationBody.text = firstTerminalText;
                    if (isCoroutineRunning == false)
                    {
                        //Debug.Log("QU'EST-CE QUE LE FUCK 3");
                        StartCoroutine(ClearTextAfterSeconds(notificationBody, 5.0f));
                    }
                }
                break;

            case NotificationType.TERMINAL_2_DONE:
                if (notificationBody != null)
                {
                    notificationBody.text = secondTerminalText;
                    if (isCoroutineRunning == false)
                    {
                        Debug.Log("QU'EST-CE QUE LE FUCK 4");
                        StartCoroutine(ClearTextAfterSeconds(notificationBody, 5.0f));
                    }
                }
                break;

            case (NotificationType.DISPLAY_TERMINAL_DIRECTIONS):
                if (notificationBody != null)
                {
                    notificationBody.text = directions;
                    if (isCoroutineRunning == false)
                    {

                        Debug.Log("QU'EST-CE QUE LE FUCK 5");
                        //StartCoroutine(ClearTextAfterSeconds(notificationBody, 5.0f));
                    }
                }
                break;

            case NotificationType.GAME_OVER:
                LockMouse(false);
                TurnOffPanel(hudPanelGroup, containerCanvas);
                TurnOnPanel(gameOverPanelGroup, containerCanvas);
                break;
        }
    }

    public void DisplayHealthStats(float healthValue)
    {
        healthValue /= 100f;
        health_stats.fillAmount = healthValue;
    }

    public void DisplayStaminaStats(float staminaValue)
    {
        staminaValue /= 100f;
        stamina_stats.fillAmount = staminaValue;
    }

    private void LockMouse(bool mouseState) // Locks / unlocks the mouse + hard pauses the game.
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

    private IEnumerator ClearTextAfterSeconds(TextMeshProUGUI textObject, float seconds)
    {
        print("UCK THIS COLIJBLIVLIYV");
        isCoroutineRunning = true;
        yield return new WaitForSeconds(seconds);
        textObject.text = "";
        isCoroutineRunning = false;
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