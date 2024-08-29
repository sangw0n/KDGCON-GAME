// # System
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Stage Info", menuName = "ScriptableObject/StageInfo")]
public class StageInfo : ScriptableObject
{
    public string   titleName;
    public Sprite   stageImage;
    [TextArea]
    public string   explanation;
}