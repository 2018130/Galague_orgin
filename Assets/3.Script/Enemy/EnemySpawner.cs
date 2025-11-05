using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject bossPrefab;

    private EnemyController[] pool;

    private int poolSize = 10;

    [SerializeField]
    private GameObject hpBarPrefab;

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private StageData stageData;

    [SerializeField]
    private float spawnTime;

    public static bool IsBossStage = false;

    private void Awake()
    {
        pool = new EnemyController[poolSize];

        for(int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab).GetComponent<EnemyController>();
            pool[i].gameObject.SetActive(false);
        }

        StartCoroutine(SpawnEnemy_co());
    }

    private IEnumerator SpawnEnemy_co()
    {
        WaitForSeconds returnInstance = new WaitForSeconds(spawnTime);

        while(true)
        {
            float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);

            Vector3 position = new Vector3(positionX, stageData.LimitMax.y + 1f, 0f);

            EnemyController enemyController = GetPooledEnemy();
            enemyController.transform.position = position;
            enemyController.gameObject.SetActive(true);
            SpawnEnemy_HP(enemyController);

            yield return returnInstance;
        }
    }

    public EnemyController GetPooledEnemy()
    {
        for(int i = 0; i < pool.Length; i++)
        {
            if(!pool[i].IsSpawned)
            {
                pool[i].IsSpawned = true;
                return pool[i];
            }
        }

        EnemyController[] newPool = new EnemyController[pool.Length + 1];
        for(int i = 0; i < pool.Length; i++)
        {
            newPool[i] = pool[i];
        }
        newPool[newPool.Length - 1] = Instantiate(enemyPrefab).GetComponent<EnemyController>();

        pool = newPool;

        newPool[newPool.Length - 1].IsSpawned = true;
        return newPool[newPool.Length - 1];
    }

    public static void ReturnEnemyToPool(EnemyController enemyController)
    {
        enemyController.IsSpawned = false;
        enemyController.gameObject.SetActive(false);
    }

    private void SpawnEnemy_HP(EnemyController enemy)
    {
        GameObject hpBarClone = Instantiate(hpBarPrefab, canvas.transform);
        hpBarClone.transform.localScale = Vector3.one;

        hpBarClone.GetComponent<EnemyHPViewer>().Setup(enemy);
        hpBarClone.GetComponent<EnemyHPBar_PositionSetter>().Setup(enemy.gameObject);
    }

    public void StartBossStage()
    {
        StopAllCoroutines();

        for(int i = 0; i < pool.Length; i++)
        {
            ReturnEnemyToPool(pool[i]);
        }

        GameObject boss = Instantiate(bossPrefab);
        IsBossStage = true;
    }
}