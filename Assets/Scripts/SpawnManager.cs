using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] enemyPrefab;
    public int poolSize = 10;
    private float spawnRange = 10;
    private float startDelay = 2;
    private float spawnInterval = 2f;
    public TMP_Text enemyCounter;

    private List<GameObject> pooledEnemies = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AddToPool();
        InvokeRepeating("SpawnRandomEnemy", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AddToPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            int randomIndex = Random.Range(0, enemyPrefab.Length);
            GameObject enemy = Instantiate(enemyPrefab[randomIndex], Vector3.zero, Quaternion.identity);
            enemy.SetActive(false);  
            pooledEnemies.Add(enemy);
        }
    }
    GameObject GetInactiveEnemy()
    {
        foreach (GameObject enemy in pooledEnemies)
        {
            if (!enemy.activeInHierarchy)
                return enemy;
        }

        return null;
    }

    void SpawnRandomEnemy()
    {

        GameObject enemy = GetInactiveEnemy(); 

        if (enemy != null)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRange, spawnRange), 1, Random.Range(-spawnRange, spawnRange));
            enemy.transform.position = spawnPos;
            enemy.transform.rotation = Quaternion.identity;
            enemy.SetActive(true);
            UpdateEnemyCounter();

            ClickDestroy clickScript = enemy.GetComponent<ClickDestroy>();
            if (clickScript != null)
            {
                clickScript.spawnManager = this;
            }
        }     
    }

    public void UpdateEnemyCounter()
    {
        int activeCount = 0;

        foreach (GameObject enemy in pooledEnemies)
        {
            if (enemy.activeInHierarchy)
                activeCount++;
        }

        enemyCounter.text = "Enemigos: " + activeCount.ToString();

    }

}
