using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int poolSize = 50;
    [SerializeField] int beginningPoolSize = 5;
    [SerializeField] int poolSizeIncreaseAmount = 1;
    [SerializeField] float intervalBetweenEachPoolSizeIncrease = 5f;
    int currentPoolSize;
    [SerializeField] float spawnTimer = 3f;

    GameObject[] pool;

    void Awake()
    {
        currentPoolSize = beginningPoolSize;
        PopulatePool();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(IncreasePoolSize());
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for(int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    void EnableObjectInPool()
    {
        for(int i = 0; i < currentPoolSize; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator IncreasePoolSize()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalBetweenEachPoolSizeIncrease);
            if(currentPoolSize < poolSize)
            {
                currentPoolSize += poolSizeIncreaseAmount;
            } 
        }
    }

    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            Debug.Log("EnemyEnabled");
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer / currentPoolSize * 10);
        }
    }

}
