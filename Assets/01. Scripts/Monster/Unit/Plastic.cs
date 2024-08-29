using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Plastic : Monster
{
    [Header("Plastic Monster Info")]
    [SerializeField] private float dashDistance = default;
    [SerializeField] private float dashSpeed = default;
    [SerializeField] private float monsterScale = default;

    private float originSpeed = default;

    protected override void Start()
    {
        base.Start();

        originSpeed = moveSpeed;

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
        collider.GetComponent<Player>().TakeDamage(monsterData.Damage);
    }
}
