using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 100;
    
    public bool CreateTower(GameObject tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();
        CostWarning costWarning = FindObjectOfType<CostWarning>();

        if(bank == null)
        {
            return false;
        }

        if (bank.CurrentBalance >= cost) {
            Instantiate(tower.gameObject, position, Quaternion.identity);
            bank.Withdraw(cost);
            return true;
        }
        if (costWarning != null)
        {
            costWarning.Warning(cost, bank.CurrentBalance);
        }
        return false;
    }

}
