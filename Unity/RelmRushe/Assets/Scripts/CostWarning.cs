using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CostWarning : MonoBehaviour
{
    [SerializeField] TextMeshPro label;
    int publicCost = 0;
    int publicCash = 0;
    int difference = 0;
    bool isWarning = false;

    // Start is called before the first frame update
    void Start()
    {
        label.text = "";
    }

    // Update is called once per frame
    public void Warning(int cost, int totalCash)
    {
        Debug.Log("warning");
        if (!isWarning)
        {
            publicCost = cost;
            publicCash = totalCash;
            difference = cost - totalCash;
            StartCoroutine(DoWarning());
        }
    }

    IEnumerator DoWarning()
    {
        isWarning = true;
        label.text = "This tower costs " + publicCost.ToString() + ".";
        yield return new WaitForSeconds(1f);
        label.text = "You need " + difference.ToString() + " more to buy this tower.";
        yield return new WaitForSeconds(1f);
        label.text = "";
        isWarning = false;
    }
}
