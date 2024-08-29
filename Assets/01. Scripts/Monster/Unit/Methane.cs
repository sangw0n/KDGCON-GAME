using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Methane : Monster
{
    [Header("Methane Monster Info")]
    [SerializeField] private GameObject bulletObject = null;
    [SerializeField] private float monsterScale = default;

    

    protected override void Start()
    {
        base.Start();

        monsterAction += AttackAction;
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

    private void AttackAction()
    {
        Vector2 direction = targetTransform.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    
        Quaternion bulletAngle = Quaternion.Euler(0, 0, angle);

        Instantiate(bulletObject, transform.position, bulletAngle);
    }
}