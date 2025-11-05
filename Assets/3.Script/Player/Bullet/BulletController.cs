using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private bool isPlayer = false;

    [SerializeField]
    private StageData stageData;

    private float destroyWeight= 2.0f;

    private PlayerScore playerScore;

    private void OnEnable()
    {
        if(playerScore == null)
            playerScore = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScore>();
    }

    private void LateUpdate()
    {
        bool checkX = (stageData.LimitMax.x + destroyWeight <= transform.position.x)
            || (stageData.LimitMin.x - destroyWeight >= transform.position.x);
        bool checkY = (stageData.LimitMax.y + destroyWeight <= transform.position.y)
            || (stageData.LimitMin.y - destroyWeight >= transform.position.y);

        if(checkX || checkY)
        {
            //BossController.ReturnToPool(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어의 공격
        if(collision.CompareTag("Enemy") && isPlayer)
        {
            collision.GetComponent<EnemyController>().TakeDamage(1);
            playerScore.SetScore(1);
            Destroy(gameObject);
        }

        if(collision.CompareTag("Player") && !isPlayer)
        {
            collision.GetComponent<PlayerController>().TakeDamage(1);
            BossController.ReturnToPool(gameObject);
        }
    }
}
