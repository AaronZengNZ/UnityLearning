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
    [SerializeField] string example;
    [SerializeField] string englishExample;
    [SerializeField] int correctbtn;
    [SerializeField] GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        if (other.gameObject.tag == "MainCamera")
        {
            canvas.GetComponent<QuestionScript>().ShowQuestion(word, question, btn1, btn2, btn3, btn4, example, englishExample, correctbtn, () =>
            {
                gameObject.SetActive(false);
            });
        }
    }
}