using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int attackPower = 30;
    public float criticalChance = 0.2f; // 20% 확률로 크리티컬
    public float criticalMultiplier = 2.0f; // 크리티컬 시 데미지 2배
    public float maxHp;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
