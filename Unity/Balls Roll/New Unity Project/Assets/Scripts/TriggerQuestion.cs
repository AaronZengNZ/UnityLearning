using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerQuestion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void OnTriggerEnter()
    {
        QuestionScript.Instance.ShowQuestion("Murder", () =>
        {
            gameObject.SetActive(false);
        });
    }
}
