using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureManager : MonoBehaviour
{
    public static TemperatureManager Instance { get; private set; }

    public GameObject clearPanel;

    [SerializeField]
    public float earthTemperature;     // 지구 온도 

    public float EarthTemperature { get => earthTemperature; }

    private bool isIncreasing = true; // 온도 증가 여부를 확인하는 플래그

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Constants.ExtremeDangerTemperature로 초기화
        earthTemperature = Constants.ExtremeDangerTemperature;
    }

    private void Start()
    {
        // 2초마다 지구 온도를 1씩 증가시키는 반복 호출 시작
        InvokeRepeating(nameof(IncreaseEarthTemperature), 2.0f, 2.0f);
    }

    public void DecreaseEarthTemperature(float value)
    {
        earthTemperature -= value;

        // 지구 온도가 0 이하로 내려가지 않도록 제한
        if (earthTemperature < 0)
        {
            earthTemperature = 0;
        }

        // 온도가 0이 되면 증가 멈추기
        if (earthTemperature == 0)
        {
            isIncreasing = false;
            CancelInvoke(nameof(IncreaseEarthTemperature));
        }
    }

    // 지구 온도를 1씩 증가시키는 메서드
    private void IncreaseEarthTemperature()
    {
        if (isIncreasing && earthTemperature < 100)
        {
            earthTemperature += 1;
            // 지구 온도가 100 이상으로 올라가지 않도록 제한
            if (earthTemperature >= 100)
            {
                earthTemperature = 100;
                // 온도가 100에 도달하면 증가 멈추기
                isIncreasing = false;
                CancelInvoke(nameof(IncreaseEarthTemperature));
            }
        }
    }

    private void Update()
    {
        if (clearPanel == null)
        {
            clearPanel = GameObject.FindWithTag("ClearPanel"); // 여기서 "ClearPanel"는 clearPanel의 태그
        }

        if (earthTemperature <= 0 && clearPanel != null)
        {
            clearPanel.transform.localScale = new Vector3(1, 1, 1);

        }
    }
}
