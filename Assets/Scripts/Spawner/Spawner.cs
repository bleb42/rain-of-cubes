using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour, ISpawner where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 10;

    public string SpawnedObjectName { get; private set; }
    
    protected ObjectPool<T> Pool;
    private int _totalCreated;

    protected virtual void Awake()
    {
        SpawnedObjectName = _prefab.name;

        Pool = new ObjectPool<T>(
                    createFunc: () => Instantiate(_prefab),
                    actionOnGet: (obj) => ActivateOnGet(obj),
                    actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                    actionOnDestroy: (obj) => Destroy(obj),
                    collectionCheck: true,
                    defaultCapacity: _poolCapacity,
                    maxSize: _poolMaxSize);
    }

    protected virtual void ActivateOnGet(T obj)
    {
        _totalCreated++;
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
}