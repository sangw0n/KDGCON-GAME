// # System
using System.Collections;
using System.Collections.Generic;
using TMPro;

// # Unity
using UnityEngine;
using UnityEngine.UI;

public class TemperatureManager : MonoBehaviour
{
    public static TemperatureManager Instance { get; private set; }

    [SerializeField]
    public float earthTemperature;     // 지구 온도 

    public float EarthTemperature { get => earthTemperature; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        earthTemperature = Constants.ExtremeDangerTemperature;
    }

    public void DecreaseEarthTemperature(float value)
    {
        earthTemperature -= value;
    }
}
