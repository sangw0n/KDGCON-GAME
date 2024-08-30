using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageListManager : MonoBehaviour
{
    public static StageListManager Instance { get; private set; } 

    [SerializeField]
    private List<Stage> stages = new List<Stage>();

    [SerializeField]
    private StageInfo[] stageInfoData;

    public StageInfo[] StageInfoData { get => stageInfoData; }

    public void Awake()
    {
        Instance = this;
    }

    public List<Stage> GetStageList()
    {
        return stages;
    }

    [ContextMenu("UpdateStageInfo")]
    private void UpdateStageInfo()
    {
        Debug.Log("정보 업데이트");

        foreach(var stage in stages)
        {
            if(GameManager.Instance.IsGameClear && stage.name == GameManager.Instance.CurrentStageName)
            {
                stage.StageClear();
                GameManager.Instance.GameClear(false);
            }
        }

        bool stageAllClear = true;
        foreach (var stage in stages)
        {
            if (stage.IsClaer == false)
            {
                stageAllClear = false;
                break;
            }
        }

        if(stageAllClear)
        {
            GameManager.Instance.SetRandomStageIndex();

            for(int i = 0; i < GameManager.Instance.StageIndexs.Length; i++)
            {
                stages[i].StageReset();
                stages[i].SetStageInfo(stageInfoData[GameManager.Instance.StageIndexs[i]]);
            }
        }
    }
}
