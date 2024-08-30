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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var playerScript = collision.GetComponent<Player>();
            playerScript.oilPanel.SetActive(false);
            playerScript.oilPanel.SetActive(true);

            playerScript.TakeDamage(2);

            Destroy(gameObject);
        }
    }


}
