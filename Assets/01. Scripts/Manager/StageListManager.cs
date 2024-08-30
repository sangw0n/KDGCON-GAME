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

        // �� �ε� �̺�Ʈ ���
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        GameObject randomButton = this.button[Random.Range(0, 3)];
        Transform pos = spawnPos[Random.Range(0, 3)].transform;

        // ��ư�� Canvas�� �ڽ����� ����
        GameObject button = Instantiate(randomButton, canvas.transform, false);

        // ���� ��ǥ�� ��ũ�� ��ǥ�� ��ȯ
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, pos.position);

        // ��ũ�� ��ǥ�� Canvas�� ���� ��ǥ�� ��ȯ
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(),
            screenPoint,
            Camera.main,
            out Vector2 localPoint
        );

        // ��ư�� RectTransform ��ġ ����
        RectTransform rectTransform = button.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = localPoint;
        rectTransform.localScale = Vector3.one; // ������ �ʱ�ȭ

        // ��ư �ʱ�ȭ
        button.GetComponent<Stage>().Initialize(stageInfoUi, stageImage, stageEntryButton, stageInfoHideButton, titleText, explanationText);

        // ��ư�� Raycast�� ���� �� �ְ� ����
        button.GetComponent<Image>().raycastTarget = true;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            // �� �ε� �̺�Ʈ ����
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "02. Battle Base")
        {
            GameObject randomButton = this.button[Random.Range(0, 3)];
            Transform pos = spawnPos[Random.Range(0, 3)].transform;

            // ��ư�� Canvas�� �ڽ����� ����
            GameObject button = Instantiate(randomButton, canvas.transform, false);

            // ���� ��ǥ�� ��ũ�� ��ǥ�� ��ȯ
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, pos.position);

            // ��ũ�� ��ǥ�� Canvas�� ���� ��ǥ�� ��ȯ
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.GetComponent<RectTransform>(),
                screenPoint,
                Camera.main,
                out Vector2 localPoint
            );

            // ��ư�� RectTransform ��ġ ����
            RectTransform rectTransform = button.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = localPoint;
            rectTransform.localScale = Vector3.one; // ������ �ʱ�ȭ

            // ��ư �ʱ�ȭ
            button.GetComponent<Stage>().Initialize(stageInfoUi, stageImage, stageEntryButton, stageInfoHideButton, titleText, explanationText);

            // ��ư�� Raycast�� ���� �� �ְ� ����
            button.GetComponent<Image>().raycastTarget = true;
        }
    }
}