using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // TODO : 화면 밖으로 나가면 사라짐

    // TODO : player와 충돌 로직 정의

    [SerializeField]
    private PlayerController player;

    [SerializeField]
    protected StageData stageData;

    private float destroyWeight = 2f;

    public bool IsSpawned = false;

    [SerializeField]
    private float maxHP = 2f;
    public float MaxHP => maxHP;

    [SerializeField]
    private float currentHP;
    public float CurrentHP => currentHP;

    private SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        currentHP = maxHP;
        spriteRenderer.color = Color.white;
    }

    protected virtual void Update()
    {
        if(transform.position.y < stageData.LimitMin.y - destroyWeight)
        {
            OnDie();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            OnDie();

            player.TakeDamage(1f);
        }
    }

    public void OnDie()
    {
        EnemySpawner.ReturnEnemyToPool(this);
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        StopCoroutine("HitAcimation_co");
        StartCoroutine("HitAcimation_co");

        if(currentHP <= 0)
        {
            OnDie();
        }
    }

    private IEnumerator HitAcimation_co()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.color = Color.white;
    }
}
