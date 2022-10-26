using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    float time = 1f;
    public DialougeTrigger creator;
    bool yes = true;
    // Start is called before the first frame update
    void Start()
    {
        timeText.text = "12 years";
    }

    public void ChangeTime(float time)
    {
        this.time = time;

        timeText.text = Mathf.Round(this.time * 12).ToString() + " years";
        if(time <= 0.01 && yes)
        {
            yes = false;
            creator.TriggerDialouge();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
