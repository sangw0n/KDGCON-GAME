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
    [SerializeField] float attackCoolTime; // ���� ��Ÿ��
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

        // ������ ���� �ִϸ��̼�
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
            { // ���� Ȱ��ȭ�� ���� �ִϸ��̼ǿ� ���� Ʈ���Ÿ� ����
                if (isAttack1Active)
                {
                    anim.SetTrigger("isAttack1");
                }
                else
                {
                    anim.SetTrigger("isAttack2");
                }

                // ���� ���� ȣ��� �� �ٸ� �ִϸ��̼��� Ȱ��ȭ�ϵ��� ����
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
                bool isCritical = Random.value < playerStats.criticalChance; // ũ��Ƽ�� Ȯ�� ���
                int damage = isCritical ? Mathf.RoundToInt(playerStats.attackPower * playerStats.criticalMultiplier) : playerStats.attackPower;

                collider.GetComponent<Monster>().TakeDamage(damage);

                // ������ x�� y ��ġ�� ����
                float randomX = Random.Range(-0.4f, 0.4f);
                float randomY = Random.Range(-0.4f, 0.4f);

                // ������ z ��ġ�� �����Ͽ� ������ ��ġ�� ����
                Vector3 randomPosition = collider.transform.position + new Vector3(randomX, randomY, -2);

                // ũ��Ƽ�� ���ο� ���� �ٸ� �ؽ�Ʈ ����
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
