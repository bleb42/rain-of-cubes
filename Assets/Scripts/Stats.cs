using TMPro;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _spawnerDehaviour;
    [SerializeField] private TextMeshProUGUI _text;

    private ISpawner _spawner;

    private void Awake()
    {
        if (_spawnerDehaviour is ISpawner spawner)
        {
            _spawner = spawner;
        }
    }

    private void Update()
    {
        int created = _spawner.GetTotalCount();
        int active = _spawner.GetActiveCount();

        _text.text = $"{_spawner.SpawnedObjectName} Created: {created}\n" +
                          $"{_spawner.SpawnedObjectName} Active: {active}";
    }
}