using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Oil : Monster
{
    [Header("Oil Monster Info")]
    [SerializeField] private GameObject bulletObject = null;

    

    protected override void Start()
    {
        base.Start();

        monsterAction += AttackAction;
    }

    private void AttackAction()
    {
        Vector2 direction = targetTransform.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    
        Quaternion bulletAngle = Quaternion.Euler(0, 0, angle);

        Instantiate(bulletObject, transform.position, bulletAngle);
    }
}