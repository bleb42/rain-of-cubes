using System;
using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _repeatRate;

    private Coroutine _spawningCubes;
    private bool _isSpawning = false;
    
    public event Action<Cube> CubeDied;
    public event Action StatusChanged;

    private void Start()
    {
        _spawningCubes = StartCoroutine(SpawningCubes());
    }

    protected override void ActivateOnGet(Cube cube)
    {
        base.ActivateOnGet(cube);
        cube.Died += OnCubeDie;

        Vector3 position = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)].transform.position;
        cube.Init(position);
    }

    private void OnCubeDie(Cube cube)
    {
        Pool.Release(cube);
        cube.Died -= OnCubeDie;
        CubeDied?.Invoke(cube);
        StatusChanged?.Invoke();
    }

    private void GetObject()
    {
        Pool.Get();
        StatusChanged?.Invoke();
    }

    private IEnumerator SpawningCubes() 
    {
        WaitForSeconds repeatRate = new WaitForSeconds(_repeatRate);
        _isSpawning = true;

        while (_isSpawning)
        {
            GetObject();

            yield return repeatRate;
        }
    }
}