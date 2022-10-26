using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public GameObject canvas;

    public GameObject[] commons;
    public float commonsUnlocked = 6;

    public GameObject[] rares;
    public float raresUnlocked = 1;

    public GameObject[] epics;
    public float epicsUnlocked = 0;

    public GameObject[] legendaries;
    public float legendariesUnlocked = 0;

    public float commonChances = 90f;
    public float rareChances = 8f;
    public float epicChances = 1f;
    public float legendaryChances = 1f;

    public Transform wayPoint1;
    public Transform wayPoint2;
    public Transform wayPoint3;

    public bool free = false;

    public int amount = 2;

    public float waitTime = 0.1f;

    GameObject[] placedUpgrades = new GameObject[3];

    void Start()
    {
        canvas.SetActive(false);
        if (free)
        {
            InitiateUpgrade();
        }
    }

    public void InitiateUpgrade()
    {
        canvas.SetActive(true);
        PlaceUpgrades();
        //set timescale to be 0
        Time.timeScale = 0;
    }

    public void UseGun(string name)
    {
        //find a gameobject named player
        GameObject player = GameObject.Find("Player");
        //call equip gun with name
        player.GetComponent<Player>().EquipGun(name);
        EndSelection();
    }

    public void PlaceUpgrades()
    {
        StartCoroutine(waitAndPlaceUpgrades());
    }

    IEnumerator waitAndPlaceUpgrades(){
        placedUpgrades = new GameObject[3];
        for (int i = 0; i < amount; i++)
        {
            UnityEngine.Debug.Log("Success");
            UnityEngine.Debug.Log(i);
            //wait for seconds real time 1f
            yield return new WaitForSecondsRealtime(waitTime);
            placedUpgrades[i] = Instantiate(ProcessRandom(), new Vector3(0, 0, 0), Quaternion.identity);
            placedUpgrades[i].transform.SetParent(canvas.transform);
            placedUpgrades[i].transform.position = GetWayPoint(i).position;
        }
    }

    public Transform GetWayPoint(int index)
    {
        //return the waypoint at index
        if (index == 0)
        {
            return wayPoint1;
        }
        if (index == 1)
        {
            return wayPoint2;
        }
        if (index == 2)
        {
            return wayPoint3;
        }
        return wayPoint1;
    }

    public GameObject ProcessRandom()
    {
        //process a pickrandom to make sure that it isnt already in the placedupgrades array
        GameObject random = PickRandom();
        for (int i = 0; i < placedUpgrades.Length; i++)
        {
            if (placedUpgrades[i] != null)
            {
                if (placedUpgrades[i].GetComponent<Upgrade>().name == random.GetComponent<Upgrade>().name)
                {
                    return ProcessRandom();
                }
            }
        }
        return random;
    }

    public GameObject PickRandom()
    {
        float random = Random.Range(0f, commonChances + rareChances + epicChances + legendaryChances);
        if (random <= commonChances)
        {
            if (commonsUnlocked <= 0)
            {
                return PickRandom();
            }
            return commons[(int)Random.Range(0, commonsUnlocked)];
        }
        else if (random <= commonChances + rareChances)
        {
            if (raresUnlocked <= 0)
            {
                return PickRandom();
            }
            return rares[(int)Random.Range(0, raresUnlocked)];
        }
        else if (random <= commonChances + rareChances + epicChances)
        {
            if (epicsUnlocked <= 0)
            {
                return PickRandom();
            }
            return epics[(int)Random.Range(0, epicsUnlocked)];
        }
        else if (random <= commonChances + rareChances + epicChances + legendaryChances)
        {
            if (legendariesUnlocked <= 0)
            {
                return PickRandom();
            }
            return legendaries[(int)Random.Range(0, legendariesUnlocked)];
        }
        return PickRandom();
    }

    public void EndSelection()
    {
        Time.timeScale = 1;
        canvas.SetActive(false);
    }
}
