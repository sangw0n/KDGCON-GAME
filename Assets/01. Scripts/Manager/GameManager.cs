using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;  }

    [SerializeField]
    private string          currentStageName;
    [SerializeField]
    private List<Stage>     stages = new List<Stage>();
    [SerializeField]
    private int[]           stageIndexs;

    private bool            isGameClear;

    public string           CurrentStageName { get => currentStageName; }
    public int[]            StageIndexs { get => stageIndexs; }
    public bool             IsGameClear { get => isGameClear; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetStageName(string stageName)
    {
        this.currentStageName = stageName;
    }

    public void ClearGame(bool isClear)
    {
        isGameClear = isClear;
    }
}
