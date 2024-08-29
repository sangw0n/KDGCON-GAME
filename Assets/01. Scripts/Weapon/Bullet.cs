using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Button Info")]
    [SerializeField] private float bulletSpeed = default;

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
    }


}
