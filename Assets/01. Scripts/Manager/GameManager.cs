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

        SetRandomStageIndex();
    }

    private void OnEnable()
    {
        if(SceneManager.GetActiveScene().name == "02. Battle Base")
        {
            stages = StageListManager.Instance.GetStageList();
        }
    }

    public void SetStageName(string stageName)
    {
        this.currentStageName = stageName;
    }

    [ContextMenu("SetRandomStageIndex")]
    public void SetRandomStageIndex()
    {
        stageIndexs = new int[Constants.maxStageCount];

        for(int i = 0; i < stageIndexs.Length; i++)
        {
            stageIndexs[i] = Random.Range(0, StageListManager.Instance.StageInfoData.Length);
        }
    }

    public void GameClear(bool isClear)
    {
        isGameClear = isClear;
    }
}
