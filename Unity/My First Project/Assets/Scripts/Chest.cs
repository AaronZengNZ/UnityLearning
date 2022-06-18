using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject coin;
    public GameObject gem;
    public GameObject misc;
    float one = 1;
    [SerializeField] float coins = 8;
    [SerializeField] float gems = 0;

    private void OnDestroy()
    {
        for (int i = 0; i < coins; i++)
        {
            var z = Random.Range(-1f, 1f);
            Debug.Log(z);
            var power = Random.Range(4f, 10f);
            var direction = new Vector3(0f, 0f, z);
            GameObject newCoin = Instantiate(coin, transform.position, Quaternion.identity) as GameObject;
            float randomDirection = Random.Range(-1f, 1f);
        }
        for (int i = 0; i < gems; i++)
        {
            var z = Random.Range(-1f, 1f);
            Debug.Log(z);
            var power = Random.Range(4f, 10f);
            var direction = new Vector3(0f, 0f, z);
            GameObject newGem = Instantiate(gem, transform.position, Quaternion.identity) as GameObject;
            float randomDirection = Random.Range(-1f, 1f);
        }
        for (int i = 0; i < one; i++)
        {
            var z = Random.Range(-1f, 1f);
            Debug.Log(z);
            var power = Random.Range(4f, 10f);
            var direction = new Vector3(0f, 0f, z);
            GameObject newMisc = Instantiate(misc, transform.position, Quaternion.identity) as GameObject;
            float randomDirection = Random.Range(-1f, 1f);
        }
    }
}