using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public string name;
    public GameObject hoverText;
    public Button button;
    void Awake(){
        hoverText.SetActive(false);
    }
    public void GetGun(){
        //find a gameobject names Upgrade Manager
        GameObject upgradeManager = GameObject.Find("Upgrade Manager");
        //call use gun with name
        upgradeManager.GetComponent<UpgradeManager>().UseGun(name);
    }

    public void PointerEnter()
    {
        hoverText.SetActive(true);
    }

    //Detect when Cursor leaves the GameObject
    public void PointerExit()
    {
        hoverText.SetActive(false);
    }

}
