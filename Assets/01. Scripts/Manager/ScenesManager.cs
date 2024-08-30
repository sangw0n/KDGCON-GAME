// # System
using System.Collections;
using System.Collections.Generic;

// # Unity 
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance { get; private set; }

    public GameObject fadeIn;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(SceneLoad(sceneName));
    }
    IEnumerator SceneLoad(string sceneName)
    {
        fadeIn.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);

    }
}
