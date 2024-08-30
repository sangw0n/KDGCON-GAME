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
}