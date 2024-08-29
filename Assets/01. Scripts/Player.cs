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

        // 움직임 여부 애니메이션
        if (moveDirection != Vector3.zero) anim.SetBool("isMove", true);
        else                               anim.SetBool("isMove", false);

        if (x < 0)  transform.rotation = Quaternion.Euler(0, 180, 0);// 왼쪽으로 이동할 때
        else if (x > 0) transform.rotation = Quaternion.Euler(0, 0, 0);// 오른쪽으로 이동할 때

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

                // 랜덤한 x와 y 위치를 생성
                float randomX = Random.Range(-0.4f, 0.4f); // -1.0과 1.0 사이의 랜덤 값
                float randomY = Random.Range(-0.4f, 0.4f); // -1.0과 1.0 사이의 랜덤 값

                // 기존의 z 위치와 결합하여 랜덤한 위치를 생성
                Vector3 randomPosition = collider.transform.position + new Vector3(randomX, randomY, -2);

                // 인스턴스를 생성하고 위치를 설정
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
