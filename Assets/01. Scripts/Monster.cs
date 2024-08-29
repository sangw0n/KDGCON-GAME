using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


//Àü¿ì¼º ÄÚµå
public class Monster : MonoBehaviour
{
    [Header("Monster Info")]
    [SerializeField] protected float moveSpeed = default;
    [SerializeField] private float damage = default;
    [SerializeField] private float attackCoolTime = default;
    [SerializeField] private float attackDistance = default;
    [SerializeField] private float stopDistnace = default;
    [SerializeField] private float maxHp = default;
    [SerializeField] private float checkDistance = default;
    [SerializeField] private LayerMask checkLayer = default;
    [SerializeField] protected MonsterState monsterState = default;

    protected Action monsterAction = default;

    private float currentHp = default;

    private Transform targetTransform = default;

    private Rigidbody2D rb = default;

    private bool isCheck = default;

    private float currentTime = default;

    private Vector2 dir = default;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        monsterState = MonsterState.Idle;
    }

    private void Update()
    {
        CheckState();
        SetState();
    }

    private void FixedUpdate()
    {
        rb.velocity = dir * moveSpeed;
    }

    private void Flip()
    {
        if (targetTransform.position.x <= transform.position.x) transform.localRotation = Quaternion.Euler(0, 0, 0);
        else transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    private void SetState()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, checkDistance, checkLayer);

        if (player != null)
        {
            targetTransform = player.transform;
            monsterState = MonsterState.Chase;
            if ((targetTransform.position - transform.position).magnitude <= stopDistnace)
            {
                monsterState = MonsterState.Idle;
                Attack();
            }
            else if ((targetTransform.position - transform.position).magnitude <= attackDistance)
            {
                Attack();
            }
        }
        else
        {
            monsterState = MonsterState.Idle;
        }
    }

    private void CheckState()
    {
        switch (monsterState)
        {
            case MonsterState.Idle:
                {
                    Idle();
                    break;
                }
            case MonsterState.Chase:
                {
                    Chase();
                    break;
                }
        }
    }

    protected void Attack()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= attackCoolTime)
        {
            monsterAction?.Invoke();
            currentTime = 0;
        }
    }

    protected virtual void Chase()
    {
        dir = ((Vector2)targetTransform.position - (Vector2)transform.position).normalized;
        Flip();
    }

    protected virtual void Idle()
    {
        dir = Vector2.zero;
        Debug.Log("¸ØÃç");
    }

    protected virtual void TakeDamage(float damge)
    {
        currentHp -= damge;
        Debug.Log("°ø°Ý¹ÞÀ½");

        if(currentHp < 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
        Debug.Log("Á×À½");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, checkDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stopDistnace);
    }
}
