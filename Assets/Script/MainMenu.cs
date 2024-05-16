using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool anyKeyDown = false;

    private void Update()
    {
        if (Input.anyKeyDown && !anyKeyDown)
        {
            anyKeyDown = true;
            LoadGame();
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
