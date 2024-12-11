using UnityEngine;

public class SpawnOnEnter : MonoBehaviour
{
    public GameObject enemyPrefab;  // Prefab del enemigo
    public Transform[] spawnPoints; // Puntos donde los enemigos van a aparecer
    public int enemiesToSpawn = 3;  // Número de enemigos a spawnear

    private bool hasSpawned = false;  // Para asegurarnos de que los enemigos solo se spawneen una vez

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entró es el jugador
        if (other.CompareTag("Player") && !hasSpawned)
        {
            // Llama a la función de spawnear enemigos
            SpawnEnemies();
            hasSpawned = true;  // Marca como ya spawneados
            GetComponent<Collider>().enabled = false;  // Desactiva el trigger para evitar que se active de nuevo
        }
    }

    void SpawnEnemies()
    {
        // Asegúrate de que haya suficientes puntos de spawn
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            // Selecciona un punto de spawn aleatorio
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            // Crea el enemigo en la posición seleccionada
            Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
        }
    }
}
