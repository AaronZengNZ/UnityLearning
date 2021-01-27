using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    [SerializeField] int coinCost = 100;   

    public void AddDcoins(int amount)
    {
        FindObjectOfType<DcoinDisplay>().AddDcoins(amount);
    }

    public int GetDcoinCost()
    {
        return coinCost;
    }


}
