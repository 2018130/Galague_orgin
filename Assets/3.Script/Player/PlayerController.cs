using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Movement movement;

    [SerializeField]
    private StageData stageData;

    [SerializeField]
    private Weapon weapon;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    // status
    private float maxHP = 100f;
    public float MaxHP => maxHP;


    private float currentHP;
    public float CurrentHP => currentHP;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponent<Movement>();
        weapon = GetComponent<Weapon>();
        currentHP = maxHP;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(movement.moveSpeed <= 0)
        {
            movement.moveSpeed = 5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movement.MoveTo(new Vector3(x, y, 0));

        if(Input.GetKeyDown(KeyCode.Space))
        {
            weapon.StartFire();
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            weapon.StopFire();
        }
    }

    private void LateUpdate()
    {
        float interpolatedX = Mathf.Clamp(transform.position.x , stageData.LimitMin.x, stageData.LimitMax.x);
        float interpolatedY = Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y); ;
        transform.position = new Vector3(interpolatedX, interpolatedY);
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        Debug.Log($"player hp : {currentHP}");

        StopCoroutine("HitColor_Action_co");
        StartCoroutine("HitColor_Action_co");

        if (currentHP <= 0)
        {
            OnDie();
        }
    }

    private void OnDie()
    {
        SceneManager.LoadScene($"GameOver");
    }

    private IEnumerator HitColor_Action_co()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.color = Color.white;
    }
}
