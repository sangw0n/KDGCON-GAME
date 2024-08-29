using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainSaw : Monster
{
    [Header("ChainSaw Monster Info")]
    [SerializeField] private float dashDistance = default;
    [SerializeField] private float dashSpeed = default;

    private float originSpeed = default;

    protected override void Start()
    {
        base.Start();

        originSpeed = moveSpeed;

        monsterAction += AttackAction;
    }

    private void AttackAction()
    {
        StartCoroutine(Co_Dash());
    }
    protected IEnumerator Co_Dash()
    {
        moveSpeed = dashSpeed;
        yield return new WaitForSeconds(dashDistance);
        moveSpeed = originSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(monsterData.Damage);
        }
    }
}
