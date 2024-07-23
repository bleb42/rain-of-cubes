using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color[] _colors;
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;

    public event Action<Cube> Died;

    private Renderer _renderer;
    private bool _haveTouchedFloor;

    private Coroutine _dieTimer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform) & _haveTouchedFloor == false)
        {
            int randomColor = UnityEngine.Random.Range(0, _colors.Length);
            _renderer.material.color = _colors[randomColor];

            _haveTouchedFloor = true;

            _dieTimer = StartCoroutine(DieTimer());
        }
    }

    public void Spawn(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);

        _renderer.material.color = _defaultColor; 
        _haveTouchedFloor = false;
    }

    private IEnumerator DieTimer() 
    {
        WaitForSeconds second = new WaitForSeconds(1);

        float lifeTime = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);
        float currentTime = 0f;

        while (currentTime <= lifeTime) 
        {
            currentTime++;

            yield return second;
        }

        Died?.Invoke(this);
    }
}