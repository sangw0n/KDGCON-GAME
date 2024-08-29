using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    private Plastic plastic = null;

    private void Awake()
    {
        plastic = GetComponentInParent<Plastic>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            plastic.OnChildColliderEnter(collision);
        }
    }
}
