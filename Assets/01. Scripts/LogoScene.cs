using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoScene : MonoBehaviour
{
    public float sceneLoadTime;
    public string sceneName;

    void Start()
    {
        Invoke("Scene", sceneLoadTime);
    }

    void Scene()
    {
        SceneManager.LoadScene(sceneName);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
