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
        RotateDangerLine();  // ���輱(dangerLine) ȸ��
    }

    private void AttackAction()
    {
        dashDir = ((Vector2)targetTransform.position - (Vector2)transform.position).normalized;
        Debug.Log(dashDir);
        StartCoroutine(Co_Dash());
    }
    protected IEnumerator Co_Dash()
    {
        // Rigidbody2D ������Ʈ�� �����ɴϴ�.
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Rigidbody2D�� �������� ������(Freeze)�Ͽ� ü���� �������� �ʵ��� �����մϴ�.
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        dangerLine.SetActive(true);
        yield return new WaitForSeconds(1.5f); // ��� ��ũ�� ǥ�õǴ� �ð� ���� ��ٸ�
        dangerLine.SetActive(false);

        // ����� �����Ͽ� ��ø� ������ �� �ֵ��� �����մϴ�.
        rb.constraints = RigidbodyConstraints2D.None;

        isDash = true;
        dashAction += Dash;
        yield return new WaitForSeconds(dashDistance); // ��� �ð��� ����� ������ ��ٸ�
        isDash = false;
        dashAction -= Dash;

        // ��ð� ���� �� �ٽ� �̵��� ȸ���� ������(Freeze)���� �ʵ��� �����մϴ�.
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
        // dashDir ������ ������ ����Ͽ� dangerLine�� ȸ���� ����
        float angle = Mathf.Atan2(dashDir.y, dashDir.x) * Mathf.Rad2Deg;
        dangerLine.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
