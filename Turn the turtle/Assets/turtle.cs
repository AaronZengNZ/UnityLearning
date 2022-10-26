using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turtle : MonoBehaviour
{
    public float amountOfScreenShakes = 0f;
    bool screenCanBeShaken = true;
    DialougeTrigger dialougeTrigger;
    public DialougeTrigger dialougeTrigger2;
    public DialougeTrigger dialougeTrigger3;
    public DialougeTrigger dialougeTrigger4;
    public GameObject shakeButton;
    [SerializeField] AudioSource bom;
    float warningNumber = 0f;
    public float ClicksUntilWarning = 10f;

    void Start()
    {
        dialougeTrigger = GetComponent<DialougeTrigger>();
    }

    public void IncreaseScreenShakes()
    {
        if (screenCanBeShaken)
        {
            bom.Play();
            amountOfScreenShakes++;
        }
    }

    void Update()
    {
        if(amountOfScreenShakes > ClicksUntilWarning)
            warningNumber += 1f;        {

            if (warningNumber == 1)
            {
                dialougeTrigger.TriggerDialouge();
            }
            if (warningNumber == 2)
            {
                dialougeTrigger2.TriggerDialouge();
            }
            if (warningNumber == 3)
            {
                dialougeTrigger3.TriggerDialouge();
            }
            if (warningNumber == 4)
            {
                dialougeTrigger4.TriggerDialouge();
                screenCanBeShaken = false;
                shakeButton.SetActive(false);
            }
            amountOfScreenShakes = 0f;
        }
    }


}
