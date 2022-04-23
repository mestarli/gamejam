using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn_Controller : MonoBehaviour
{
    // Variables
    [SerializeField] private GameObject enemyPrefab;
    
    [SerializeField] private List<Transform> arraySpawnPoints;

    private int randomIndex_ArraySpawnPoints;
    private Transform currentSpawnPoint;

    private bool isGameOver;
    private int limitedEnemiesSpawned;
    private int limitTotalEnemiesSpawned = 5;

    // Start is called before the first frame update
    void Start()
    {
        // Manifesto de la coroutine
        StartCoroutine(GenerateEnemies());
    }

    /// <summary>
    /// Coroutine que se ejecuta cada X segundos. Llamando a los métodos privados de SelectRandomSpawnVirusPosition y
    /// InstantiateVirus
    /// </summary>
    /// <returns> La generación constante de un meteorito aleatorio en un spawn aleatorio </returns>
    IEnumerator GenerateEnemies()
    {
        // Coroutine que se ejecuta cada 10 segundos
        // También manifiesta el SelectRandomSpawnPosition y el InstantiateEnemy

        while (!isGameOver)
        {
            if (limitedEnemiesSpawned < limitTotalEnemiesSpawned)
            {
                SelectRandomSpawnPosition();
                InstantiateEnemy();
                yield return new WaitForSeconds(10);
            }

            else if (limitedEnemiesSpawned == limitTotalEnemiesSpawned)
            {
                isGameOver = true;
            }
        }
    }

    /// <summary>
    /// Este método genera un número desde 0 a la longitud del array de spawns y almacena un elemento GameObject
    /// que indicamos a partir del index aleatorio
    /// </summary>
    private void SelectRandomSpawnPosition()
    {
        // Num. aleatorio de 0 hasta la longitud actual del array de spwans
        randomIndex_ArraySpawnPoints = Random.Range(0, arraySpawnPoints.Count);

        // Actualización de los gameObject currentSpawnpoint para asociar la posición de los spawn
        currentSpawnPoint = arraySpawnPoints[randomIndex_ArraySpawnPoints];
    }

    /// <summary>
    /// Este método genera un número aleatorio desde 0 a la longitud del array de enemies y almacena un elemento
    /// GameObject que indicamos a partir del index aleatorio. Seguidamente creamos un enemigo en la posición y
    /// rotación indicada
    /// </summary>
    private void InstantiateEnemy()
    {
        var startPos = currentSpawnPoint.position;
        GameObject obj = Instantiate(enemyPrefab, startPos, Quaternion.identity);
    }
}
