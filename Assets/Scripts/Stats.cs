using TMPro;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private TextMeshProUGUI _statsText;

    private void Update()
    {
        int cubesCreated = _cubeSpawner.GetTotalCount();
        int cubesActive = _cubeSpawner.GetActiveCount();
        int bombsCreated = _bombSpawner.GetTotalCount();
        int bombsActive = _bombSpawner.GetActiveCount();

        _statsText.text = $"Cubes Created: {cubesCreated}\n" +
                          $"Cubes Active: {cubesActive}\n" +
                          $"Bombs Created: {bombsCreated}\n" +
                          $"Bombs Active: {bombsActive}";
    }
}