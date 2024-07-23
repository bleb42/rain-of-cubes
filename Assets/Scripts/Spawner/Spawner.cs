using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 10;

    protected ObjectPool<T> _pool;
    
    private int _totalCreated;

    protected virtual void Awake()
    {
        _pool = new ObjectPool<T>(
                    createFunc: () => Instantiate(_prefab),
                    actionOnGet: (obj) => ActionOnGet(obj),
                    actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                    actionOnDestroy: (obj) => Destroy(obj),
                    collectionCheck: true,
                    defaultCapacity: _poolCapacity,
                    maxSize: _poolMaxSize);
    }

    protected virtual void ActionOnGet(T obj)
    {
        _totalCreated++;
    }

    public int GetActiveCount()
    {
        return _pool.CountActive;
    }

    public int GetTotalCount()
    {
        return _totalCreated;
    }
}