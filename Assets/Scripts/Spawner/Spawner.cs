using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour, ISpawner where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 10;

    protected ObjectPool<T> Pool;
    private int _totalCreated;

    public event Action StatusChanged;

    public string SpawnedObjectName { get; private set; }

    protected virtual void Awake()
    {
        SpawnedObjectName = _prefab.name;

        Pool = new ObjectPool<T>(
                    createFunc: () => Instantiate(_prefab),
                    actionOnGet: (obj) => ActivateOnGet(obj),
                    actionOnRelease: (obj) => ActivateOnRelease(obj),
                    actionOnDestroy: (obj) => Destroy(obj),
                    collectionCheck: true,
                    defaultCapacity: _poolCapacity,
                    maxSize: _poolMaxSize);
    }

    protected virtual void ActivateOnGet(T obj)
    {
        _totalCreated++;
        StatusChanged?.Invoke();
    }

    protected virtual void ActivateOnRelease(T obj)
    {
        obj.gameObject.SetActive(false);
        StatusChanged?.Invoke();
    }

    public int GetActiveCount()
    {
        return Pool.CountActive;
    }

    public int GetTotalCount()
    {
        return _totalCreated;
    }
}

public interface ISpawner
{
    string SpawnedObjectName { get; }
    int GetTotalCount();
    int GetActiveCount();

    event Action StatusChanged;
}