using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryEnemy : MonoBehaviour
{
    [SerializeField] private float maxHp;
    [SerializeField] private float curHp;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Material normalMaterial;  // �߸��� ������ ����
    [SerializeField] private Material hitMaterial;     // �߸��� ������ ����

    [SerializeField] private GameObject exp;

    private bool isDead = false; // ���� �׾����� ���θ� üũ�ϴ� �÷���

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        curHp = maxHp;
    }

    void Update()
    {
        if (curHp <= 0 && !isDead)
        {
            isDead = true;  // ���� �׾����� ǥ��
            StartCoroutine(Exp());
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return; // �̹� �׾��ٸ� �߰��� �������� ���� ����

        curHp -= damage;
        CameraShake.instance.Shake(0.16f, 0.07f);
        StartCoroutine(HitMaterialCor());
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

        Destroy(gameObject);  // �� ������Ʈ ����
    }
}
