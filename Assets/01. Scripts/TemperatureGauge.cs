// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class TemperatureGauge : MonoBehaviour
{
    [SerializeField]
    private float earthTemperature;     // 지구 온도 

    public void DecreaseEarthTemperature(float value)
    {
        earthTemperature -= value;


    }
}
