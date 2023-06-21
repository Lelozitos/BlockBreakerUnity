using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void changeToGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void changeToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void changeToSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
