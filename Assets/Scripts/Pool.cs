using UnityEngine;
using UnityEngine.Pool;

public class Pool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private GameObject[] _spawnPoints;
    [SerializeField] private float _repeatRate = 1f;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 10;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
                    createFunc: () => Instantiate(_cubePrefab),
                    actionOnGet: (obj) => ActionOnGet(obj),
                    actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                    actionOnDestroy: (obj) => Destroy(obj),
                    collectionCheck: true,
                    defaultCapacity: _poolCapacity,
                    maxSize: _poolMaxSize);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _repeatRate);
    }
    
    private void ActionOnGet(Cube cube)
    {
        int randomSpawnpoint = Random.Range(0, _spawnPoints.Length);

        cube.Died += OnCubeDie;
        cube.transform.position = _spawnPoints[randomSpawnpoint].transform.position;

        cube.Spawn();
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private void OnCubeDie(Cube cube)
    {
        _pool.Release(cube);
        cube.Died -= OnCubeDie;
    }
}
