using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionScript : MonoBehaviour
{
    public static QuestionScript Instance { get; private set; }
    public TextMeshProUGUI textMeshPro;
    public TextMeshProUGUI textMeshPro2;
    public Button btn1;
    public Button btn2;
    public Button btn3;
    public Button btn4;
    public TextMeshProUGUI tmp1;
    public TextMeshProUGUI tmp2;
    public TextMeshProUGUI tmp3;
    public TextMeshProUGUI tmp4;
    public GameObject canvas;
    public TextMeshProUGUI txt1;
    public TextMeshProUGUI txt2;
    public TextMeshProUGUI txt3;
    public TextMeshProUGUI txt4;
    public TextMeshProUGUI txt5;
    string exampler;
    string correctAnswer;
    string englishExample;
    int btnnum;
    private bool questionWrong = false;

    // Start is called before the first frame update
    void Awake()
    {
        Hide();
        Instance = this;
        questionWrong = false;

    }
    
    void Start() { 
        canvas.SetActive(false);
        HideTxts();
    }

    // Update is called once per frame
    public void ShowQuestion(string questionText, string descriptionText, string text1, string text2, string text3, string text4, string exemplar, string englishexample, int correctBtn, Action hideQuestion)
    {
        Show();
        textMeshPro.text = questionText;
        textMeshPro2.text = descriptionText;
        tmp1.text = text1;
        tmp2.text = text2;
        tmp3.text = text3;
        tmp4.text = text4;
        exampler = exemplar;
        englishExample = englishexample;
        btnnum = correctBtn;
        btn1.onClick.AddListener(() =>
        {
            
            CheckQuestion(1, correctBtn, exemplar, text1, englishExample);
            if (correctBtn == 1)
            {
                Hide();
                hideQuestion();
            }

        });
        btn2.onClick.AddListener(() =>
        {
            
            CheckQuestion(2, correctBtn, exemplar, text2, englishExample);
            if (correctBtn == 2)
            {
                Hide();
                hideQuestion();
            }
        });
        btn3.onClick.AddListener(() =>
        {
            
            CheckQuestion(3, correctBtn, exemplar, text3, englishExample);
            if(correctBtn == 3)
            {
                Hide();
                hideQuestion();
            }
        });
        btn4.onClick.AddListener(() =>
        {
            
            CheckQuestion(4, correctBtn, exemplar, text4, englishExample);
            if (correctBtn == 4)
            {
                Hide();
                hideQuestion();
            }
        });
    }

    void CheckQuestion(int btn, int correctanswer,string exampler, string currentanswer, string englishexample)
    {
        if(btn == correctanswer)
        {
            return;
        }
        else
        {
            canvas.SetActive(true);
            if (questionWrong == false)
            {
                StartCoroutine(WrongAnswer(exampler, currentanswer, englishexample));
                questionWrong = true;
            }
        }
    }

    IEnumerator WrongAnswer(string example, string correctanswer, string englishexample)
    {
        canvas.SetActive(true);
        HideTxts();
        CoordinateTxts(example, correctanswer, englishexample, 1);
        yield return new WaitForSeconds(1);
        CoordinateTxts(example, correctanswer, englishexample, 2);
        yield return new WaitForSeconds(1);
        CoordinateTxts(example, correctanswer, englishexample, 3);
        yield return new WaitForSeconds(1);
        CoordinateTxts(example, correctanswer, englishexample, 4);
        yield return new WaitForSeconds(1);
        CoordinateTxts(example, correctanswer, englishexample, 5);
        yield return new WaitForSeconds(1);
        if(btnnum > 4)
        {
            CoordinateTxts(example, correctanswer, englishexample, 6);
            yield return new WaitForSeconds(0.5f);
        }
        CoordinateTxts(example, correctanswer, englishexample, 1);
        CoordinateTxts(example, correctanswer, englishexample, 2);
        CoordinateTxts(example, correctanswer, englishexample, 3);
        CoordinateTxts(example, correctanswer, englishexample, 4);
        CoordinateTxts(example, correctanswer, englishexample, 5);
        yield return new WaitForSeconds(2);
        Hide();

    }

    void CoordinateTxts(string example, string correctanswer, string englishexample, int txt)
    {
        if (txt == 1) { txt1.text = "You got the question wrong!"; }
        if (txt == 2) { txt2.text = "The correct answer was " + correctanswer + "."; }
        if (btnnum > 4 && txt == 2) { txt2.text = "There was no correct answer."; }
        if (txt == 3) { txt3.text = "Example:"; }
        if (txt == 4) { txt4.text = example; }
        if (txt == 5) { txt5.text = englishexample; }
        if (txt == 6)
        {
            txt1.text = "01000100 01101111 01101110 00100111 01110100 00100000 01100010 01100101 01101100 01101001 01100101 01110110 01100101 00100000 01110111 01101000 01100001 01110100 00100000 01111001 01101111 01110101 00100000 01110011 01100101 01100101";
            txt2.text = "";
            txt3.text = "";
            txt4.text = "";
            txt5.text = "";
        }
    }

    void HideTxts()
    {
        Debug.Log("hide texts");
        txt1.text = "";
        txt2.text = "";
        txt3.text = "";
        txt4.text = "";
        txt5.text = "";
    }


    public void Hide()
    {
        Cursor.visible = false;
        gameObject.SetActive(false);
    }

    public void Show()
    {
        Cursor.visible = true;
        gameObject.SetActive(true);
    }
}
