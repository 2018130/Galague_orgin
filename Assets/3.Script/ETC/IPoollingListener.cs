using UnityEngine;

public interface IPoollingListener
{
    public void Spawn(Vector3 position);

    public void Despawn();

}