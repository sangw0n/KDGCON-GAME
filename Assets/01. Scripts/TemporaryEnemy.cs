using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TemporaryEnemy : MonoBehaviour
{
    [SerializeField] private float maxHp;
    [SerializeField] private float curHp;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Material normalMaterial;  // 잘못된 변수명 수정
    [SerializeField] private Material hitMaterial;     // 잘못된 변수명 수정

    [SerializeField] private GameObject exp;
    [SerializeField] private GameObject dieVolume;

    private bool isDead = false; // 적이 죽었는지 여부를 체크하는 플래그

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
        if (isDead) return; // 이미 죽었다면 추가로 데미지를 받지 않음

        curHp -= damage;
        CameraShake.instance.Shake(0.16f, 0.07f);
        StartCoroutine(HitMaterialCor());
        AudioManager.instance.PlaySound(transform.position, 1, Random.Range(1f, 1.4f), 0.6f);

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
    void Die()
    {
        if (curHp <= 0 && !isDead)
        {
            isDead = true;  // 적이 죽었음을 표시
            StartCoroutine(Exp());
            StartCoroutine(TimeStop());
        }
    }
}
