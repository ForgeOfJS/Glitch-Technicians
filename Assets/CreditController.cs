using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }
}