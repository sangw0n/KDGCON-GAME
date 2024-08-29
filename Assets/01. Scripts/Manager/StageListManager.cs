using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageListManager : MonoBehaviour
{
    public static StageListManager Instance { get; private set; } 

    [SerializeField]
    private List<Stage> stages = new List<Stage>();

    public void Awake()
    {
        Instance = this;
    }

    public List<Stage> GetStageList()
    {
        return stages;
    }

}
