// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;
using UnityEngine.UI;

// # Project 
using DG.Tweening;
using TMPro;

public class Stage : MonoBehaviour
{
    [SerializeField]
    private string              sceneName;
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

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        stageInfoButton.onClick.AddListener(() =>
            OnShowStageInfoUI());

        stageInfoHideButton.onClick.AddListener(() =>
            OnHideStageInfoUI());
    }

    private void InitializeInfoUI()
    {
        stageImage.sprite    = stageInfo.stageImage;
        titleText.text       = stageInfo.titleName;
        explanationText.text = stageInfo.explanation;
    }

    private void OnShowStageInfoUI()
    {
        stageEntryButton.onClick.AddListener(() =>
            ScenesManager.Instance.LoadScene(sceneName));

        InitializeInfoUI();

        Sequence mySequence = DOTween.Sequence();

        Tween tr0 = stageInfoUi.rectTransform.DOAnchorPosX(-50, 0.5f).SetEase(Ease.OutQuad);
        Tween tr1 = stageInfoUi.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.25f);
        Tween tr2 = stageInfoUi.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.1f);

        mySequence.Append(tr0);
        mySequence.Append(tr1);
        mySequence.Append(tr2);

        mySequence.Play();
    }

    private void OnHideStageInfoUI()
    {
        Sequence mySequence = DOTween.Sequence();

        Tween tr0 = stageInfoUi.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.25f);
        Tween tr1 = stageInfoUi.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.1f);
        Tween tr2 = stageInfoUi.rectTransform.DOAnchorPosX(900, 0.5f).SetEase(Ease.OutQuad);

        mySequence.Append(tr0);
        mySequence.Append(tr1);
        mySequence.Append(tr2);

        mySequence.Play();
    }
}
