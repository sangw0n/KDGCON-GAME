using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCollider : MonoBehaviour
{
    private new Collider collider;

    private void Start()
    {
        collider = GetComponent<Collider>();

        if(collider != null)
        {
            collider.enabled = false;
            collider.enabled = true;
        }
    }
}
