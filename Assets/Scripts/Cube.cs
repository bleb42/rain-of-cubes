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

    private Coroutine _dieTimer;
    private Renderer _renderer;
    private bool _haveTouchedFloor;

    public event Action<Cube> Died;

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

    public void Init(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);

        _renderer.material.color = _defaultColor; 
        _haveTouchedFloor = false;
    }

    private IEnumerator DieTimer() 
    {
        float lifeTime = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);

        yield return new WaitForSeconds(lifeTime);

        Died?.Invoke(this);
    }
}