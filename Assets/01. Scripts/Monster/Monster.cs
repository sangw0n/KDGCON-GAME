using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


//전우성 코드
public class Monster : MonoBehaviour
{
    [Header("Monster Info")]
    [SerializeField] protected float moveSpeed = default;
    [SerializeField] protected MonsterData monsterData = default;
    [SerializeField] private LayerMask checkLayer = default;
     private SpriteRenderer spriteRenderer;
        
    [SerializeField] private Material normalMaterial;  // 잘못된 변수명 수정
    [SerializeField] private Material hitMaterial;     // 잘못된 변수명 수정
    private bool isDead = false; // 적이 죽었는지 여부를 체크하는 플래그

    [SerializeField] private GameObject exp;
    [SerializeField] private GameObject dieVolume;

    protected Action monsterAction = default;
    protected Transform targetTransform = default;

    private float damage = default;
    private float attackCoolTime = default;
    private float attackDistance = default;
    private float stopDistnace = default;
    private float maxHp = default;
    private float checkDistance = default;
    public  float currentHp = default;
    private float currentTime = default;

    public MonsterState monsterState = default;
    
    
   protected Rigidbody2D rb = default;


    protected Vector2 dir = default;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHp = monsterData.MaxHp;
    }

    protected virtual void Start()
    {
        DataInitialization();

        monsterState = MonsterState.Idle;
    }

    protected virtual void Update()
    {
        CheckState();
        SetState();
        Die();
    }

    private void FixedUpdate()
    {
        rb.velocity = dir * moveSpeed;
    }

    private void DataInitialization()
    {
        Debug.Log("초기화 실행");

        damage = monsterData.Damage;
        attackCoolTime = monsterData.AttackCooolTime;
        attackDistance = monsterData.AttackDistance;
        stopDistnace = monsterData.StopDistnace;
        maxHp = monsterData.MaxHp;
        checkDistance = monsterData.CheckDistance;
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
    }

    protected virtual void Idle()
    {
        dir = Vector2.zero;
        Debug.Log("멈춰");
    }

    public virtual void TakeDamage(float damge)
    {
        if (isDead) return; // 이미 죽었다면 추가로 데미지를 받지 않음

        currentHp -= damge;  // 전달된 데미지만큼 currentHp를 감소시킵니다.
        CameraShake.instance.Shake(0.16f, 0.07f);
        StartCoroutine(HitMaterialCor());
        AudioManager.instance.PlaySound(transform.position, 1, UnityEngine.Random.Range(1f, 1.4f), 0.6f);
    }

    void Die()
    {
        if (currentHp <= 0 && !isDead)
        {
            isDead = true;  // 적이 죽었음을 표시
            StartCoroutine(Exp());
            StartCoroutine(TimeStop());
        }
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
    IEnumerator HitMaterialCor()
    {
        spriteRenderer.material = hitMaterial;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = normalMaterial;
    }
    IEnumerator Exp()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(exp, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(gameObject);  // 적 오브젝트 제거
    }
    IEnumerator TimeStop()
    {
        dieVolume.SetActive(true);
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 1;
        dieVolume.SetActive(false);

    }
}
