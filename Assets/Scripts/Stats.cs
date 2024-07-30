using TMPro;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _spawnerBehaviour;
    [SerializeField] private TextMeshProUGUI _text;

    private ISpawner _spawner;

    private void Awake()
    {
        if (_spawnerBehaviour is ISpawner spawner)
        {
            _spawner = spawner;
        }
    }

    private void Start()
    {
        UpdateStats();
    }

    private void OnEnable()
    {
        _spawner.StatusChanged += UpdateStats;
    }

    private void OnDisable()
    {
        _spawner.StatusChanged += UpdateStats;
    }

    private void UpdateStats()
    {
        int created = _spawner.GetTotalCount();
        int active = _spawner.GetActiveCount();

        _text.text = $"{_spawner.SpawnedObjectName} Created: {created}\n" +
                          $"{_spawner.SpawnedObjectName} Active: {active}";
    }
}