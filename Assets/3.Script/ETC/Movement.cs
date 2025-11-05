using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 0f;
    public Vector3 moveDirection = Vector3.zero;
    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }

    private void Update()
    {
        transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;
    }
}
