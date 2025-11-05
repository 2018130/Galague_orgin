using UnityEngine;

public class Background : MonoBehaviour
{
    private float width = 0f;

    private void Awake()
    {
        width = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void Update()
    {
        if(transform.position.y <= -width)
        {
            transform.position += new Vector3(0, width * 2);
        }
    }
}
