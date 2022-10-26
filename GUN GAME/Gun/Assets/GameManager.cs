using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public EnemySpawner[] waves;
    public float currentWave = 1f;
    public TextMeshProUGUI timeText;

    float time;

    public UpgradeManager upgradeManager;

    void Start(){
        upgradeManager = GameObject.Find("Upgrade Manager").GetComponent<UpgradeManager>();
        waves[(int)currentWave - 1].StartSpawning();
    }

    void Update(){
        if(waves[(int)currentWave - 1].spawning == false){
            currentWave += 1f;
            waves[(int)currentWave - 1].StartSpawning();
            upgradeManager.InitiateUpgrade();
            if(currentWave > waves.Length){
                //nope
            }
        }
        time += Time.deltaTime;
        //make timetext time but rounded
        timeText.text = Mathf.Round(time).ToString();
    }
}
