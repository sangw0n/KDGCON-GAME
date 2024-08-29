using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;


    [Header("Attack")]
    [SerializeField] private Transform pos;
    [SerializeField] private Vector2 boxSize;

    [SerializeField] private GameObject damageText;

    private Animator anim;

    private PlayerStats playerStats;

    private SpriteRenderer sr;



    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        Move();
        Attack();
        sr.sortingOrder = Mathf.RoundToInt(transform.position.y) * -1;

    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(x, y, 0).normalized;
        Vector3 moveVelocity = moveDirection * moveSpeed * Time.deltaTime;
        this.transform.position += moveVelocity;

        // ������ ���� �ִϸ��̼�
        if (moveDirection != Vector3.zero) anim.SetBool("isMove", true);
        else                               anim.SetBool("isMove", false);

        if (x < 0)  transform.rotation = Quaternion.Euler(0, 180, 0);// �������� �̵��� ��
        else if (x > 0) transform.rotation = Quaternion.Euler(0, 0, 0);// ���������� �̵��� ��

    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("isAttack1");
        }
    }

    public void Damage()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);

        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Enemy")
            {
                collider.GetComponent<TemporaryEnemy>().TakeDamage(playerStats.attackPower);

                // ������ x�� y ��ġ�� ����
                float randomX = Random.Range(-0.4f, 0.4f); // -1.0�� 1.0 ������ ���� ��
                float randomY = Random.Range(-0.4f, 0.4f); // -1.0�� 1.0 ������ ���� ��

                // ������ z ��ġ�� �����Ͽ� ������ ��ġ�� ����
                Vector3 randomPosition = collider.transform.position + new Vector3(randomX, randomY, -2);

                // �ν��Ͻ��� �����ϰ� ��ġ�� ����
                var damageTextob = Instantiate(damageText, randomPosition, Quaternion.identity).GetComponent<TMP_Text>();
                damageTextob.text = playerStats.attackPower.ToString();

            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }

}
