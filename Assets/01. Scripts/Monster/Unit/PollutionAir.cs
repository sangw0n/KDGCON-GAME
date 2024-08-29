using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WasteNet : Monster
{
    [Header("WasteNet Monster Info")]
    [SerializeField] private float monsterScale = default;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        Flip();
    }

    private void Flip()
    {
        if (targetTransform.position.x >= transform.position.x)
        {
            transform.localScale = new Vector3(-monsterScale, monsterScale, monsterScale);
        }
        else
        {
            transform.localScale = new Vector3(monsterScale, monsterScale, monsterScale);
        }
    }
}
