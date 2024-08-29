using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryEnemy : MonoBehaviour
{
    [SerializeField] private float maxHp;
    [SerializeField] private float curHp;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Material nomanMaterial;
    [SerializeField] private Material hitMatarial;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        curHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float damage)
    {
        curHp -= damage;
        CameraShake.instance.Shake(0.16f, 0.07f);
        StartCoroutine(HitMaterialCor());

    }
    IEnumerator HitMaterialCor()
    {
        spriteRenderer.material = hitMatarial;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = nomanMaterial;

    }
}
