using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerBulletPrefab;
    [SerializeField] private float AttackRate = 0.5f;

    private void TryAttack()
    {
        Instantiate(PlayerBulletPrefab, transform.position + Vector3.up, Quaternion.identity);
    }

    public void StartFire()
    {
        //TryAttack();

        StartCoroutine("TryAttack_Co");
    }

    public void StopFire()
    {
        StopCoroutine("TryAttack_Co");
    }

    private IEnumerator TryAttack_Co()
    {
        while(true)
        {
            TryAttack();
            yield return new WaitForSeconds(AttackRate);
        }
    }
}
