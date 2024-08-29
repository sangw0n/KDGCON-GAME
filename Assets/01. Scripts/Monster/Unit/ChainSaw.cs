using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainSaw : Monster
{
    [Header("ChainSaw Monster Info")]
    [SerializeField] private float dashDistance = default;
    [SerializeField] private float dashSpeed = default;
    [SerializeField] private float monsterScale = default;

    private Action dashAction = default;

    private Vector2 dashDir = default;

    private bool isDash = default;

    protected override void Start()
    {
        base.Start();

        monsterAction += AttackAction;
    }

    protected override void Update()
    {
        base.Update();

        if ((!isDash))
        {
            Flip();
        }

        dashAction?.Invoke();
        
    }

    private void AttackAction()
    {
        dashDir = ((Vector2)targetTransform.position - (Vector2)transform.position).normalized;
        Debug.Log(dashDir);
        StartCoroutine(Co_Dash());
    }
    protected IEnumerator Co_Dash()
    {
        isDash = true;
        dashAction += Dash;
        yield return new WaitForSeconds(dashDistance);
        isDash = false;
        dashAction -= Dash;
    }

    private void Dash()
    {
        dir = Vector2.zero;
        transform.Translate(dashDir * dashSpeed * Time.deltaTime);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(monsterData.Damage);
        }
    }
}
