using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void Start()
    {
        _cubeSpawner.CubeDied += OnCubeDied;
    }

    protected override void ActionOnGet(Bomb bomb)
    {
        base.ActionOnGet(bomb);
        bomb.Died += OnBombDie;
    }

    private void OnBombDie(Bomb bomb)
    {
        _pool.Release(bomb);
        bomb.Died -= OnBombDie;
    }

    private void OnCubeDied(Cube cube)
    {
        Bomb bomb = _pool.Get();
        bomb.Spawn(cube.transform.position);
        bomb.StartFading();
    }
}