using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureSlider : MonoBehaviour
{
    [SerializeField]
    private Slider temperatureSlider;
    [SerializeField]
    private TMP_Text temperatureText;

    private void Update()
    {
        temperatureSlider.value = Mathf.Lerp(temperatureSlider.value,TemperatureManager.Instance.earthTemperature / Constants.ExtremeDangerTemperature, Time.deltaTime * 40f);
        temperatureText.text = TemperatureManager.Instance.earthTemperature.ToString();
    }
}
