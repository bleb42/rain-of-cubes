using System;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _repeatRate;

    public event Action<Cube> CubeDied;

    private void Start()
    {
        InvokeRepeating(nameof(GetObject), 0.0f, _repeatRate);
    }

    protected override void ActionOnGet(Cube cube)
    {
        base.ActionOnGet(cube);
        cube.Died += OnCubeDie;

        Vector3 position = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)].transform.position;
        cube.Spawn(position);
    }

    private void OnCubeDie(Cube cube)
    {
        _pool.Release(cube);
        cube.Died -= OnCubeDie;
        CubeDied?.Invoke(cube);
    }

    private void GetObject()
    {
        _pool.Get();
    }
}