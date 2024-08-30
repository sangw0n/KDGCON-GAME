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

    [SerializeField] GameObject dangerLine;

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
        RotateDangerLine();  // 위험선(dangerLine) 회전
    }

    private void AttackAction()
    {
        dashDir = ((Vector2)targetTransform.position - (Vector2)transform.position).normalized;
        Debug.Log(dashDir);
        StartCoroutine(Co_Dash());
    }
    protected IEnumerator Co_Dash()
    {
        // Rigidbody2D 컴포넌트를 가져옵니다.
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Rigidbody2D의 움직임을 프리즈(Freeze)하여 체인이 움직이지 않도록 설정합니다.
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        dangerLine.SetActive(true);
        yield return new WaitForSeconds(1.5f); // 대시 마크가 표시되는 시간 동안 기다림
        dangerLine.SetActive(false);

        // 프리즈를 해제하여 대시를 시작할 수 있도록 설정합니다.
        rb.constraints = RigidbodyConstraints2D.None;

        isDash = true;
        dashAction += Dash;
        yield return new WaitForSeconds(dashDistance); // 대시 시간이 경과할 때까지 기다림
        isDash = false;
        dashAction -= Dash;

        // 대시가 끝난 후 다시 이동과 회전을 프리즈(Freeze)하지 않도록 설정합니다.
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }


    private void Dash()
    {
        dir = Vector2.zero;
        transform.Translate(dashDir * dashSpeed * Time.deltaTime);
    }

    private void Flip()
    {
        // Get the SpriteRenderer component attached to the same GameObject
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Check the position of the target relative to the monster
        if (targetTransform.position.x >= transform.position.x)
        {
            // If the target is on the right, flip the sprite to face right
            spriteRenderer.flipX = false;
        }
        else
        {
            // If the target is on the left, flip the sprite to face left
            spriteRenderer.flipX = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(monsterData.Damage);
        }
    }

    private void RotateDangerLine()
    {
        // dashDir 벡터의 각도를 계산하여 dangerLine의 회전을 설정
        float angle = Mathf.Atan2(dashDir.y, dashDir.x) * Mathf.Rad2Deg;
        dangerLine.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
