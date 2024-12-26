using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class MainMenuScript : MonoBehaviour
{
    public static bool isGamePaused = false;

    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject settingsMenu;

    void Start()
    {
        Resume();
        settingsMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyBindings.KeyCodes[KeyBindings.KeyCode_Esc]))
        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        menu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    void Pause()
    {
        menu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void RestartSection()
    {
        Debug.Log("Restarting");
        Resume();
        StartCoroutine(loadThis(SceneManager.GetActiveScene().name));
    }

    static public bool isFullyLoaded;
    IEnumerator loadThis(string sceneName)
    {
        isFullyLoaded = false;
        SceneManager.LoadScene(sceneName);
        isFullyLoaded = true;

        yield return null;
    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }

    public void ExitGame()
    {
        Resume();
        Debug.Log("Exiting the game");
        Application.Quit();
    }
}
