using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject shieldPrefab;
    [SerializeField] float spawnSpeed = 10f;
    [SerializeField] float addedRandomness = 5f;

    private void Start()
    {
        StartCoroutine(WaitAndSpawn());
    }

    IEnumerator WaitAndSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnSpeed + Random.Range(0, addedRandomness));
            transform.position = new Vector3(Random.Range(0, 9), transform.position.y, transform.position.z);
            Instantiate(shieldPrefab, transform.position, transform.rotation);
        }
    }

}
