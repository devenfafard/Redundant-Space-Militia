using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel = null;
    [SerializeField] private GameObject creditsPanel = null;

    private void Awake()
    {
        mainMenuPanel.SetActive(true);
        creditsPanel.SetActive(false);
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
}
