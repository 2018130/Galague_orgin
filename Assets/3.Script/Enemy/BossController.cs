using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    private Movement movement;

    [Header("Movement")]
    [SerializeField] private float minChangeDirTime = 0.5f;
    [SerializeField] private float maxChangeDirTime = 1.2f;

    [Header("Bullet")]
    [SerializeField] private GameObject enemyBulletPrefab;
    [SerializeField] private float bulletShootingTime = 0.5f;
    private GameObject[] pool;


    protected override void Awake()
    {
        base.Awake();
        movement = GetComponent<Movement>();
        pool = new GameObject[30];
        for(int i = 0; i < 30; i++)
        {
            pool[i] = Instantiate(enemyBulletPrefab);
            pool[i].SetActive(false);
        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected void Start()
    {
        StartCoroutine(ChangeDir_co());
        StartCoroutine(ShootBullet_co());
        StartCoroutine(ShootSkill_01_co());
        StartCoroutine(ShootSkill_02_co());
    }

    private void Update()
    {
        //Shoot();
    }
    private void LateUpdate()
    {
        float interpolatedX = Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x);
        float interpolatedY = Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y); ;
        transform.position = new Vector3(interpolatedX, interpolatedY);
    }

    private IEnumerator ChangeDir_co()
    {
        while (true)
        {
            if(Mathf.Approximately(movement.moveDirection.x, -1))
            {
                movement.MoveTo(Vector3.right);
            }
            else
            {
                movement.MoveTo(Vector3.left);
            }

            yield return new WaitForSeconds(UnityEngine.Random.Range(minChangeDirTime, maxChangeDirTime));
        }
    }

    private IEnumerator ShootBullet_co()
    {
        while(true)
        {
            GameObject bullet = Instantiate(enemyBulletPrefab, transform.position + Vector3.down, Quaternion.identity);

            yield return new WaitForSeconds(bulletShootingTime);
        }
    }

    #region
    private IEnumerator ShootSkill_01_co()
    {
        while (true)
        {
            ShootSkill_01();

            yield return new WaitForSeconds(10f);
        }
    }

    private void ShootSkill_01()
    {
        float offset = 2f;
        float bulletDist = 0.5f;
        int randIdx = UnityEngine.Random.Range(0, 10);
        for (int i = 0; i < 10; i++)
        {
            if (i == randIdx)
                continue;

            float posX =  bulletDist * i - offset;
            GameObject clone = GetObjectFromPool();
            clone.transform.position = new Vector3(posX, transform.position.y - 0.3f);
        }
    }
    private IEnumerator ShootSkill_02_co()
    {
        while (true)
        {
            ShootSkill_02(1);

            yield return new WaitForSeconds(0.5f);

            ShootSkill_02(2);

            yield return new WaitForSeconds(0.5f);

            ShootSkill_02(3);



            yield return new WaitForSeconds(7f);
        }
    }

    private void ShootSkill_02(int bulletCount)
    {
        float bulletDist = 0.5f;

        for (int i = 0; i < bulletCount; i++)
        {
            float posX =  -(i * bulletDist);
            GameObject clone = GetObjectFromPool();
            clone.transform.position = new Vector3(posX, transform.position.y - 0.3f);

            posX = i * bulletDist;
            clone = GetObjectFromPool();
            clone.transform.position = new Vector3(posX, transform.position.y - 0.3f);
        }
    }

    #endregion
    #region pooling
    public static void ReturnToPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    private GameObject GetObjectFromPool()
    {
        for(int i = 0; i < pool.Length; i++)
        {
            if(!pool[i].activeSelf)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }

        GameObject[] newPool = new GameObject[pool.Length + 1];
        for(int i = 0; i < pool.Length; i++)
        {
            newPool[i] = pool[i];
        }
        GameObject newClone = Instantiate(enemyBulletPrefab);
        newPool[newPool.Length - 1] = newClone;
        return newClone;
    }
    #endregion
}
