using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float priority = 1f;
    float temp1;
    public GameObject enemy2;
    public float priority2 = 0f;
    float temp2;
    float tempP2;
    public float timeSig2 = 0f;
    public GameObject enemy3;
    public float priority3 = 0f;

    public bool instasummon = true;
    float temp3;
    float tempP3;
    public float timeSig3 = 0f;
    public float spawnRate = 1f;
    public float yRange = 5f;

    public float combinedPriority;
    public float combinedTime;
    float randNum = 1f;
    float time = 0f;

    public bool spawning = false;
    public float maxDuration = 30f;
    public float currentTime = 0f;

    GameObject[] spawnedObjects;
    void Start()
    {
        temp1 = priority;
        temp2 = priority2;
        temp3 = priority3;
        tempP2 = priority2;
        tempP3 = priority3;
        combinedPriority = priority + priority2 + priority3;
        if (timeSig2 > 0f)
        {
            priority2 = 0f;
        }
        if (timeSig3 > 0f)
        {
            priority3 = 0f;
        }
    }

    public void StartSpawning(){
        spawning = true;
        StartCoroutine(Spawn());
    }

    void Update(){
        currentTime += Time.deltaTime;
    }

    IEnumerator Spawn()
    {
        while (time < maxDuration)
        {
            yield return new WaitForSeconds(spawnRate);
            time += spawnRate;
            if (time > timeSig3)
            {
                priority3 = tempP3;
                //set time sig 3 to infinity
                timeSig3 = Mathf.Infinity;
            }
            if (time > timeSig2)
            {
                priority2 = tempP2;
                //set time sig 2 to infinity
                timeSig2 = Mathf.Infinity;
            }
            spawnEnemy();
        }
        //wait until there is no enemy
        while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            yield return new WaitForSeconds(0.1f);
        }
        spawning = false;
    }

    public void spawnEnemy()
    {
        randNum = Random.Range(1, priority + priority2 + priority3);
        Vector2 spawnPos = new Vector2(transform.position.x, transform.position.y + Random.Range(-yRange, yRange));
        if (randNum <= priority)
        {
            if (temp1 >= 1)
            {
                Instantiate(enemy, spawnPos, Quaternion.identity);
                temp1 -= 1;
            }
            else
            {
                spawnEnemy();
                return;
            }
        }
        if (randNum <= priority2 + priority && randNum > priority)
        {
            if (temp2 >= 1)
            {
                Instantiate(enemy2, spawnPos, Quaternion.identity);
                temp2 -= 1;
            }
            else
            {
                spawnEnemy();
                return;
            }
        }
        if (randNum <= priority3 + priority2 + priority && randNum > priority2 + priority)
        {
            if (temp3 >= 1)
            {
                Instantiate(enemy3, spawnPos, Quaternion.identity);
                temp3 -= 1;
            }
            else
            {
                spawnEnemy();
                return;
            }
        }
        if (temp1 + temp2 + temp3 <= 0)
        {
            temp1 = priority;
            temp2 = priority2;
            temp3 = priority3;
        }
    }
}