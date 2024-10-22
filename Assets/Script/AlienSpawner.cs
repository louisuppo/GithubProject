using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{

    public GameObject enemyPrefab; // Référence au prefab de l'ennemi
    public float spawnDelay = 2f; // Délai entre les spawns
    public Vector2 spawnAreaSize = new Vector2(10, 10); // Taille de la zone de spawn

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnEnemy()
    {
        // Calculer une position aléatoire dans la zone de spawn
        Vector2 randomPosition = new Vector2(
            transform.position.x + Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            transform.position.y + Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
        );

        // Instancier l'ennemi à la position aléatoire
        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }
}

