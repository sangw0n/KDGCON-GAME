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
        RotateDangerLine();  // 위험선(dangerLine) 회전
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
        // Rigidbody2D 컴포넌트를 가져옵니다.
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Rigidbody2D의 움직임을 프리즈(Freeze)하여 체인이 움직이지 않도록 설정합니다.
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        yield return new WaitForSeconds(0.5f);
        dangerLine.SetActive(true);
        yield return new WaitForSeconds(1.5f); // 대시 마크가 표시되는 시간 동안 기다림
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
        // dashDir 벡터의 각도를 계산하여 dangerLine의 회전을 설정
        float angle = Mathf.Atan2(dashDir.y, dashDir.x) * Mathf.Rad2Deg;
        dangerLine.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}