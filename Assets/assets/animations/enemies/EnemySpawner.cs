using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo
    public Transform[] spawnPoints; // Array de puntos de spawn
    public float spawnInterval = 5f; // Intervalo de tiempo entre los spawns

    private void Start()
    {
        // Comienza a invocar enemigos de forma repetitiva
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Elegir un punto de spawn aleatorio
        int randomIndex = Random.Range(0, spawnPoints.Length);

        // Instanciar al enemigo en el punto de spawn aleatorio
        Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
    }
}
