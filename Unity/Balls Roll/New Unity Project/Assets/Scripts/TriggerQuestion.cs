using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerQuestion : MonoBehaviour
{
    [SerializeField] string word;
    [SerializeField] string question;
    [SerializeField] string btn1;
    [SerializeField] string btn2;
    [SerializeField] string btn3;
    [SerializeField] string btn4;
    [SerializeField] int correctbtn;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void OnTriggerEnter()
    {
        QuestionScript.Instance.ShowQuestion(word, question, btn1, btn2, btn3, btn4, correctbtn, () =>
        {
            gameObject.SetActive(false);
        });
    }
}
