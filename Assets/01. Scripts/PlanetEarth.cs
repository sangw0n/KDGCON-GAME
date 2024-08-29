// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class PlanetEarth : MonoBehaviour
{
    [SerializeField]
    private Color[] colors;

    private void Start()
    {
        UpdateEarth(TemperatureManager.Instance.EarthTemperature);
    }

    public void UpdateEarth(float temperatureValue)
    {
        if (temperatureValue >= Constants.ExtremeDangerTemperature)
        {
            transform.GetComponent<SpriteRenderer>().color = colors[0];
        }
        else if (temperatureValue >= Constants.HighRiskTemperature)
        {
            transform.GetComponent<SpriteRenderer>().color = colors[1];
        }
        else if (temperatureValue >= Constants.ModerateRiskTemperature)
        {
            transform.GetComponent<SpriteRenderer>().color = colors[2];
        }
        else if (temperatureValue >= Constants.LowLiskTemperature)
        {
            transform.GetComponent<SpriteRenderer>().color = colors[3];
        }
        else // Constants.SafeTemperature
        {
            transform.GetComponent<SpriteRenderer>().color = colors[4];
        }
    }
}
