// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;
using UnityEngine.UI;

// # Project 
using DG.Tweening;
using TMPro;
using UnityEngine.Rendering.LookDev;

public class Stage : MonoBehaviour
{
    [SerializeField]
    private string              sceneName;
    [SerializeField]
    private string              stageName;
    [SerializeField]
    private bool                isClear;

    [SerializeField]
    private StageInfo           stageInfo;

    [Header("UI Settings")]
    [SerializeField]
    private Image              stageInfoUi;
    [SerializeField]
    private Image              stageImage;

    [Space(10), SerializeField]
    private Button              stageEntryButton;
    [SerializeField]
    private Button              stageInfoButton;
    [SerializeField]
    private Button              stageInfoHideButton;

    [Space(10), SerializeField]
    private TextMeshProUGUI     titleText;
    [SerializeField]
    private TextMeshProUGUI     explanationText;

    public StageInfo            StageInfo { get => stageInfo; }
    public string               StageName { get => stageName; }
    public bool                 IsClaer { get => isClear;  }

    public void Initialize(Image stageInfoUI, Image stageImage, Button stageEntryButton, Button HideButton, TextMeshProUGUI titleText, TextMeshProUGUI explantionText)
    {
        this.stageInfoUi = stageInfoUI;
        this.stageImage = stageImage;
        this.stageEntryButton = stageEntryButton;
        this.stageInfoHideButton = HideButton;
        this.titleText = titleText;
        this.explanationText = explantionText;

        stageInfoButton.onClick.AddListener(() =>
            OnShowStageInfoUI());

        stageInfoHideButton.onClick.AddListener(() =>
            OnHideStageInfoUI());

        sceneName = stageInfo.sceneName;
        stageName = stageInfo.stageName;
    }

    private void InitializeInfo()
    {
        sceneName = stageInfo.sceneName;
        stageName = stageInfo.stageName;

        GameManager.Instance.SetStageName(stageName);
    }

    private void InitializeInfoUI()
    {
        stageImage.sprite    = stageInfo.stageImage;
        titleText.text       = stageInfo.titleName;
        explanationText.text = stageInfo.explanation;
    }

    private void OnShowStageInfoUI()
    {
        InitializeInfo();
        InitializeInfoUI();

        ScreenTouch.Instance.isOpenUI = true;

        stageEntryButton.onClick.RemoveAllListeners();

        stageEntryButton.onClick.AddListener(() =>
            ScenesManager.Instance.LoadScene(sceneName));

        Sequence mySequence = DOTween.Sequence();

        Tween tr0 = stageInfoUi.rectTransform.DOAnchorPosX(-40, 0.5f).SetEase(Ease.OutQuad);
        Tween tr1 = stageInfoUi.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.25f);
        Tween tr2 = stageInfoUi.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.1f);

        mySequence.Append(tr0);
        mySequence.Append(tr1);
        mySequence.Append(tr2);

        mySequence.Play();
    }

    private void OnHideStageInfoUI()
    {
        ScreenTouch.Instance.isOpenUI = false;

        Sequence mySequence = DOTween.Sequence();

        Tween tr0 = stageInfoUi.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.25f);
        Tween tr1 = stageInfoUi.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.1f);
        Tween tr2 = stageInfoUi.rectTransform.DOAnchorPosX(900, 0.5f).SetEase(Ease.OutQuad);

        mySequence.Append(tr0);
        mySequence.Append(tr1);
        mySequence.Append(tr2);

        mySequence.Play();
    }

    public void StageClear()
    {
        isClear = true;
    }

    public void SetStageInfo(StageInfo stageInfo)
    {
        this.stageInfo = stageInfo;
    }

    public void StageReset()
    {
        isClear = false;
    }
}
