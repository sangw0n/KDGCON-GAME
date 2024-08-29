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
    private float maxEarthTemperature;
    [SerializeField]
    private float earthTemperature;     // 지구 온도 

    [SerializeField]
    private Slider temperatureSlider;
    [SerializeField]
    private TMP_Text temperatureText;
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
        earthTemperature = maxEarthTemperature;
    }
    private void Update()
    {
        temperatureSlider.value = Mathf.Lerp(temperatureSlider.value, earthTemperature / maxEarthTemperature, Time.deltaTime * 40f);
        temperatureText.text = earthTemperature.ToString();
    }
    public void DecreaseEarthTemperature(float value)
    {
        earthTemperature -= value;
    }
}
