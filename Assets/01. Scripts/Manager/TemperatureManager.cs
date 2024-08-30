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
    public float earthTemperature;     // ���� �µ� 

    public float EarthTemperature { get => earthTemperature; }

    private bool isIncreasing = true; // �µ� ���� ���θ� Ȯ���ϴ� �÷���

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

        // Constants.ExtremeDangerTemperature�� �ʱ�ȭ
        earthTemperature = Constants.ExtremeDangerTemperature;
    }

    private void Start()
    {
        // 2�ʸ��� ���� �µ��� 1�� ������Ű�� �ݺ� ȣ�� ����
        InvokeRepeating(nameof(IncreaseEarthTemperature), 2.0f, 2.0f);
    }

    public void DecreaseEarthTemperature(float value)
    {
        earthTemperature -= value;

        // ���� �µ��� 0 ���Ϸ� �������� �ʵ��� ����
        if (earthTemperature < 0)
        {
            earthTemperature = 0;
        }

        // �µ��� 0�� �Ǹ� ���� ���߱�
        if (earthTemperature == 0)
        {
            isIncreasing = false;
            CancelInvoke(nameof(IncreaseEarthTemperature));
        }
    }

    // ���� �µ��� 1�� ������Ű�� �޼���
    private void IncreaseEarthTemperature()
    {
        if (isIncreasing && earthTemperature < 100)
        {
            earthTemperature += 1;
            // ���� �µ��� 100 �̻����� �ö��� �ʵ��� ����
            if (earthTemperature >= 100)
            {
                earthTemperature = 100;
                // �µ��� 100�� �����ϸ� ���� ���߱�
                isIncreasing = false;
                CancelInvoke(nameof(IncreaseEarthTemperature));
            }
        }
    }

    private void Update()
    {
        if (clearPanel == null)
        {
            clearPanel = GameObject.FindWithTag("ClearPanel"); // ���⼭ "ClearPanel"�� clearPanel�� �±�
        }

        if (earthTemperature <= 0 && clearPanel != null)
        {
            clearPanel.transform.localScale = new Vector3(1, 1, 1);

        }
    }
}
