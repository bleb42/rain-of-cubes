using System;
using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void Start()
    {
        _cubeSpawner.CubeDied += OnCubeDied;
    }

    protected override void ActivateOnGet(Bomb bomb)
    {
        base.ActivateOnGet(bomb);
        bomb.Died += OnBombDie;
    }

    private void OnBombDie(Bomb bomb)
    {
        Pool.Release(bomb);
        bomb.Died -= OnBombDie;
    }

    private void OnCubeDied(Cube cube)
    {
        Bomb bomb = Pool.Get();
        bomb.Init(cube.transform.position);
        bomb.StartFading();
    }
}