using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TemporaryEnemy : MonoBehaviour
{
    [SerializeField] private float maxHp;
    [SerializeField] private float curHp;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Material normalMaterial;  // �߸��� ������ ����
    [SerializeField] private Material hitMaterial;     // �߸��� ������ ����

    [SerializeField] private GameObject exp;
    [SerializeField] private GameObject dieVolume;

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
        Die();
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
    IEnumerator TimeStop()
    {
        dieVolume.SetActive(true);
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 1;
        dieVolume.SetActive(false);

    }
    void Die()
    {
        if (curHp <= 0 && !isDead)
        {
            isDead = true;  // ���� �׾����� ǥ��
            StartCoroutine(Exp());
            StartCoroutine(TimeStop());
        }
    }
}
