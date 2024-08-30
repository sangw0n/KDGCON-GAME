using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageListManager : MonoBehaviour
{
    public static StageListManager Instance { get; private set; }

    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    private GameObject[] button;
    [SerializeField]
    private Transform[] spawnPos;

    [Header("UI Settings")]
    [SerializeField]
    private Image stageInfoUi;
    [SerializeField]
    private Image stageImage;

    [Space(10), SerializeField]
    private Button stageEntryButton;
    [SerializeField]
    private Button stageInfoHideButton;

    [Space(10), SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private TextMeshProUGUI explanationText;

    public void Awake()
    {
        Instance = this;

        // 씬 로드 이벤트 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        GameObject randomButton = this.button[Random.Range(0, 3)];
        Transform pos = spawnPos[Random.Range(0, 3)].transform;

        // 버튼을 Canvas의 자식으로 생성
        GameObject button = Instantiate(randomButton, canvas.transform, false);

        // 월드 좌표를 스크린 좌표로 변환
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, pos.position);

        // 스크린 좌표를 Canvas의 로컬 좌표로 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(),
            screenPoint,
            Camera.main,
            out Vector2 localPoint
        );

        // 버튼의 RectTransform 위치 설정
        RectTransform rectTransform = button.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = localPoint;
        rectTransform.localScale = Vector3.one; // 스케일 초기화

        // 버튼 초기화
        button.GetComponent<Stage>().Initialize(stageInfoUi, stageImage, stageEntryButton, stageInfoHideButton, titleText, explanationText);

        // 버튼이 Raycast를 받을 수 있게 설정
        button.GetComponent<Image>().raycastTarget = true;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            // 씬 로드 이벤트 해제
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "02. Battle Base")
        {
            GameObject randomButton = this.button[Random.Range(0, 3)];
            Transform pos = spawnPos[Random.Range(0, 3)].transform;

            // 버튼을 Canvas의 자식으로 생성
            GameObject button = Instantiate(randomButton, canvas.transform, false);

            // 월드 좌표를 스크린 좌표로 변환
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, pos.position);

            // 스크린 좌표를 Canvas의 로컬 좌표로 변환
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.GetComponent<RectTransform>(),
                screenPoint,
                Camera.main,
                out Vector2 localPoint
            );

            // 버튼의 RectTransform 위치 설정
            RectTransform rectTransform = button.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = localPoint;
            rectTransform.localScale = Vector3.one; // 스케일 초기화

            // 버튼 초기화
            button.GetComponent<Stage>().Initialize(stageInfoUi, stageImage, stageEntryButton, stageInfoHideButton, titleText, explanationText);

            // 버튼이 Raycast를 받을 수 있게 설정
            button.GetComponent<Image>().raycastTarget = true;
        }
    }
}