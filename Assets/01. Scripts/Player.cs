using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [Header("Attack")]
    [SerializeField] private Transform pos;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] float curHp;
    [SerializeField] float attackCoolTime; // 공격 쿨타임
    float attackCurTime;



    [SerializeField] private GameObject damageText;
    [SerializeField] private GameObject criticalDamageText;
    [SerializeField] private GameObject bubbleptc;
    [SerializeField] private GameObject hitptc;


    [SerializeField] Slider hpBar;

    private Animator anim;
    private PlayerStats playerStats;
    private SpriteRenderer sr;

    private bool isAttack1Active = true;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();
        curHp = playerStats.maxHp;
    }

    private void Update()
    {
        Move();
        Attack();
        sr.sortingOrder = Mathf.RoundToInt(transform.position.y) * -1;


        hpBar.value = Mathf.Lerp(hpBar.value, curHp / playerStats.maxHp, Time.deltaTime * 40f);
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(x, y, 0).normalized;
        Vector3 moveVelocity = moveDirection * moveSpeed * Time.deltaTime;
        this.transform.position += moveVelocity;

        // 움직임 여부 애니메이션
        if (moveDirection == Vector3.zero)
        {
            anim.SetBool("isSideMove", false);
            anim.SetBool("isFrontMove", false);
            anim.SetBool("isBackMove", false);
        }
        else
        {
            if (x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                anim.SetBool("isSideMove", true);
            }
            else if (x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                anim.SetBool("isSideMove", true);
            }
            else
            {
                anim.SetBool("isSideMove", false);
            }

            if (y < 0)
            {
                anim.SetBool("isFrontMove", true);
                anim.SetBool("isBackMove", false);
            }
            else if (y > 0)
            {
                anim.SetBool("isBackMove", true);
                anim.SetBool("isFrontMove", false);
            }
            else
            {
                anim.SetBool("isFrontMove", false);
                anim.SetBool("isBackMove", false);
            }
        }
    }

    void Attack()
    {
        if (attackCurTime <= 0)
        {

            if (Input.GetMouseButtonDown(0))
            { // 현재 활성화된 공격 애니메이션에 따라 트리거를 설정
                if (isAttack1Active)
                {
                    anim.SetTrigger("isAttack1");
                }
                else
                {
                    anim.SetTrigger("isAttack2");
                }

                // 다음 번에 호출될 때 다른 애니메이션을 활성화하도록 설정
                isAttack1Active = !isAttack1Active;

                attackCurTime = attackCoolTime;
                AudioManager.instance.PlaySound(transform.position, 0, Random.Range(1f, 1.6f), 0.6f);

            }
        }
        else
        {
            attackCurTime -= Time.deltaTime;
        }

    }

    public void Damage()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);

        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Enemy")
            {
                bool isCritical = Random.value < playerStats.criticalChance; // 크리티컬 확률 계산
                int damage = isCritical ? Mathf.RoundToInt(playerStats.attackPower * playerStats.criticalMultiplier) : playerStats.attackPower;

                collider.GetComponent<Monster>().TakeDamage(damage);

                // 랜덤한 x와 y 위치를 생성
                float randomX = Random.Range(-0.4f, 0.4f);
                float randomY = Random.Range(-0.4f, 0.4f);

                // 기존의 z 위치와 결합하여 랜덤한 위치를 생성
                Vector3 randomPosition = collider.transform.position + new Vector3(randomX, randomY, -2);

                // 크리티컬 여부에 따라 다른 텍스트 생성
                GameObject textPrefab = isCritical ? criticalDamageText : damageText;
                var damageTextob = Instantiate(textPrefab, randomPosition, Quaternion.identity).GetComponent<TMP_Text>();
                damageTextob.text = damage.ToString();

                Instantiate(bubbleptc, collider.transform.position, Quaternion.identity);
                Instantiate(hitptc, collider.transform.position, Quaternion.identity);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        curHp -= damage;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
}
