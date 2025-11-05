using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> GameObjectsPrefab;

    private Dictionary<string, Queue<IPoollingListener>> pool = new Dictionary<string, Queue<IPoollingListener>>();


    public T GetFromPool<T>() where T : MonoBehaviour
    {
        if (typeof(T).GetInterface("IPoollingListener") == null)
        {
            Debug.Log($"Cannot using object pool, not exist IPoollingListener interface.");
            return null;
        }

        string typeKey = typeof(T).ToString();
        if (pool.ContainsKey(typeKey) && pool[typeKey].Count > 0)
        {
            Debug.Log($"Get object from pool");
            return (T)pool[typeKey].Dequeue();
        }
        else
        {
            Debug.Log($"Create new object type : {typeof(T)}");

            GameObject prefab = GameObjectsPrefab.Find(x => x.GetComponent<T>() != null);
            if (prefab != null)
            {
                return Instantiate(prefab).GetComponent<T>();
            }
            else
            {
                Debug.Log($"No exist any prefab within {typeof(T)}");
                return null;
            }
        }
    }

    public void ReturnToPool(MonoBehaviour returnObj)
    {
        string typeKey = returnObj.GetType().ToString();
        if (pool.ContainsKey(typeKey))
        {
            Debug.Log($"Return to created. pool {returnObj.gameObject.name}");
            pool[typeKey].Enqueue(returnObj.GetComponent<IPoollingListener>());
        }
        else
        {
            Debug.Log($"Return to new pool {returnObj.gameObject.name}");
            Queue<IPoollingListener> queue = new Queue<IPoollingListener>();
            queue.Enqueue(returnObj.GetComponent<IPoollingListener>());

            pool.Add(typeKey, queue);
        }
    }

}
