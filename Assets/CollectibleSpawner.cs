using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject collectiblePrefab; // Kerättävä objekti
    public int collectibleCount = 10; // Kuinka monta spawnaa
    public Vector3 spawnAreaMin; // Spawnialueen minimi
    public Vector3 spawnAreaMax; // Spawnialueen maksimi

    // Start is called before the first frame update
    void Start()
    {
        SpawnCollectibles();
    }

    void SpawnCollectibles()
    {
        for (int i = 0; i < collectibleCount; i++)
        {
            // Luodaan satunnainen sijainti
            Vector3 randomPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                Random.Range(spawnAreaMin.z, spawnAreaMax.z)
            );

            // Instansioidaan kerättävä objekti satunnaiseen sijaintiin
            Instantiate(collectiblePrefab, randomPosition, Quaternion.identity);
        }
    }
}