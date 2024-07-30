using System;
using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = 2f;
    [SerializeField] private float _explosionRadius = 5f;

    private Coroutine _explode;
    private Renderer _renderer;
    private Material _material;
    private Color _originalColor;
    
    public event Action<Bomb> Died;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
        _originalColor = _material.color;
    }

    public void Init(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        _material.color = _originalColor;
    }

    public void StartFading()
    {
        _explode = StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float elapsed = 0f;

        while (elapsed < _fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / _fadeDuration);

            Color newColor = _originalColor;
            newColor.a = alpha;
            _material.color = newColor;

            elapsed += Time.deltaTime;
            yield return null;
        }

        Explode();
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (var collider in colliders)
        {
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(1000f, transform.position, _explosionRadius);
            }
        }

        Died?.Invoke(this);
    }
}