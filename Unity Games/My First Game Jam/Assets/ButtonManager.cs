using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    CurrencyHolder currencyHolder;
    [SerializeField] string button1twist;
    [SerializeField] string button2twist;
    [SerializeField] string button3twist;
    [SerializeField] int map = 1;

    // Start is called before the first frame update
    void Start()
    {
        currencyHolder = FindObjectOfType<CurrencyHolder>();
    }

    public void button1clicked()
    {
        currencyHolder.AddTwist(button1twist, map);
        Debug.Log("button1clciked");
        currencyHolder.FNextScene();
    }

    public void button2clicked()
    {
        currencyHolder.AddTwist(button2twist, map);
        Debug.Log("button1clciked");
        currencyHolder.FNextScene();
    }

    public void button3clicked()
    {
        currencyHolder.AddTwist(button3twist, map);
        Debug.Log("button1clciked");
        currencyHolder.FNextScene();
    }
}
