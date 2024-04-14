using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
   public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Level 1 - Copy");
    }

    public void RollCredits()
    {
        SceneManager.LoadSceneAsync("Credits");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
