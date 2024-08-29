using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Plastic : Monster
{
    [Header("Plastic Monster Info")]
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

    public void OnChildColliderEnter(Collider2D collider)
    {
        collider.GetComponent<TestJWSPlayer>().TakeDamge(monsterData.Damage);
    }
}
