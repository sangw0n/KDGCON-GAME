using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void GameQuit()
    {
        Application.Quit();
    }

    public void GameStart()
    {
        SceneManager.LoadScene("");
    }
}
