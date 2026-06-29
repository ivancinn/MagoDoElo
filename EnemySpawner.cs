using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefabs de Enemigos")]
    public GameObject[] enemyPrefabs;
    public GameObject bossPrefab;

    [Header("Configuración de Tiempo")]
    public float spawnInterval = 2f;
    public int totalEnemiesBeforeBoss = 15;

    [Header("Movimiento del Spawner")]
    public float spawnerSpeed = 5f;
    public float xLimit = 10f;

    [Header("Posición de Aparición")]
    public float spawnYPosition = 0f;
    public float spawnZPosition = 20f;

    private int enemiesSpawnedCount = 0;
    private bool bossSpawned = false;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    void Update()
    {
        // Movimiento a prueba de fallos con Mathf.PingPong
        if (!bossSpawned)
        {
            // Calcula la posición X rebotando entre 0 y el doble del límite, 
            // y luego le resta el límite para centrarlo (ej: de -10 a 10)
            float pingPongX = Mathf.PingPong(Time.time * spawnerSpeed, xLimit * 2) - xLimit;

            // Asigna la posición exacta ignorando físicas o rotaciones previas
            transform.position = new Vector3(pingPongX, spawnYPosition, spawnZPosition);
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (enemiesSpawnedCount < totalEnemiesBeforeBoss)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnNormalEnemy();
        }

        yield return new WaitForSeconds(spawnInterval * 1.5f);

        if (!bossSpawned)
        {
            SpawnBoss();
        }
    }

    void SpawnNormalEnemy()
    {
        if (enemyPrefabs.Length == 0) return;

        int randomIndex = Random.Range(0, enemyPrefabs.Length);

        // Toma la posición exacta del spawner en este frame
        Vector3 spawnPosition = transform.position;

        Instantiate(enemyPrefabs[randomIndex], spawnPosition, Quaternion.identity);
        enemiesSpawnedCount++;
    }

    void SpawnBoss()
    {
        bossSpawned = true;

        // El jefe ignora el PingPong y aparece siempre en el centro (X = 0)
        Vector3 spawnPosition = new Vector3(0f, spawnYPosition, spawnZPosition);
        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
    }
}