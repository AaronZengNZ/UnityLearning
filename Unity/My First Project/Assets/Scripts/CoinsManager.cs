using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour
{
    public static CoinsManager instance;

    public Text scoreText;
    public Text gemText;

    int coins = 0;
    int gems = 0;
    int keys = 0;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Coins : " + coins.ToString();
        gemText.text = "Gems : " + gems.ToString();
    }

    public void PickUpGem(int value)
    {
        gems += value;
        gemText.text = "Gems : " + gems.ToString();
    }

    public void PickUpCoin(int value)
    {
        coins += value;
        scoreText.text = "Coins : " + coins.ToString();
    }

    public void PickUpKey()
    {
        keys += 1;
    }

    public bool HasKey()
    {
        if(keys > 0)
        {
            keys -= 1;
            return true;
        }
        else
        {
            return false;
        }
    }
}
