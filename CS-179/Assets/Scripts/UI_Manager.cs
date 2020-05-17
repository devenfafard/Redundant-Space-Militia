using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel = null;
    [SerializeField] private GameObject creditsPanel  = null;
    [SerializeField] private GameObject pausePanel    = null;

    private void Awake()
    {
        if (mainMenuPanel.gameObject != null)
        {
            mainMenuPanel.SetActive(true);
        }
        if (creditsPanel.gameObject != null)
        {
            creditsPanel.SetActive(false);
        }
        if(pausePanel.gameObject != null)
        {
            pausePanel.SetActive(false);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.BackQuote))
        {
            // Pause
            if(!pausePanel.activeInHierarchy)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
                pausePanel.SetActive(true);
            }
            else
            {
                OnResumeClicked();
            }
        }
    }
    public void OnPlayClicked()
    {
        // Loads Level 1, which is at build index 1.
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void OnCreditsClicked()
    {
        creditsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }

    public void OnBackClicked()
    {
        mainMenuPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    public void OnResumeClicked()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
