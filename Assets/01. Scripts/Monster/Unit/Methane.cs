using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Methane : Monster
{
    [Header("Methane Monster Info")]
    [SerializeField] private GameObject bulletObject = null;
    [SerializeField] private float monsterScale = default;

    [SerializeField] GameObject dangerLine;

    private Vector2 dashDir = default;

    protected override void Start()
    {
        base.Start();

        monsterAction += AttackAction;
    }

    protected override void Update()
    {
        base.Update();
        RotateDangerLine();  // ���輱(dangerLine) ȸ��
        dashDir = ((Vector2)targetTransform.position - (Vector2)transform.position).normalized;

        Flip();
    }

    private void Flip()
    {
        // Get the SpriteRenderer component attached to the same GameObject
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Check the position of the target relative to the monster
        if (targetTransform.position.x >= transform.position.x)
        {
            // If the target is on the right, flip the sprite to face right
            spriteRenderer.flipX = true;
        }
        else
        {
            // If the target is on the left, flip the sprite to face left
            spriteRenderer.flipX = false;
        }
    }


    private void AttackAction()
    {
        StartCoroutine(Stop());

    }

    IEnumerator Stop()
    {
        // Rigidbody2D ������Ʈ�� �����ɴϴ�.
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Rigidbody2D�� �������� ������(Freeze)�Ͽ� ü���� �������� �ʵ��� �����մϴ�.
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        yield return new WaitForSeconds(0.5f);
        dangerLine.SetActive(true);
        yield return new WaitForSeconds(1.5f); // ��� ��ũ�� ǥ�õǴ� �ð� ���� ��ٸ�
        dangerLine.SetActive(false);
        Vector2 direction = targetTransform.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion bulletAngle = Quaternion.Euler(0, 0, angle);

        Instantiate(bulletObject, transform.position, bulletAngle);
        yield return new WaitForSeconds(0.5f);

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;


    }

    private void RotateDangerLine()
    {
        // dashDir ������ ������ ����Ͽ� dangerLine�� ȸ���� ����
        float angle = Mathf.Atan2(dashDir.y, dashDir.x) * Mathf.Rad2Deg;
        dangerLine.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}