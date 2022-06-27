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

    // Start is called before the first frame update
    void Awake()
    {
        Hide();
        Instance = this;
    }

    // Update is called once per frame
    public void ShowQuestion(string questionText, string descriptionText, string text1, string text2, string text3, string text4, int correctBtn, Action hideQuestion)
    {
        Show();
        textMeshPro.text = questionText;
        textMeshPro2.text = descriptionText;
        tmp1.text = text1;
        tmp2.text = text2;
        tmp3.text = text3;
        tmp4.text = text4;
        btn1.onClick.AddListener(() =>
        {
            Hide();
            hideQuestion();
        });
        btn2.onClick.AddListener(() =>
        {
            Hide();
            hideQuestion();
        });
        btn3.onClick.AddListener(() =>
        {
            Hide();
            hideQuestion();
        });
        btn4.onClick.AddListener(() =>
        {
            Hide();
            hideQuestion();
        });
    }

    void CheckQuestion(int btn, int correctanswer)
    {
        if(btn == correctanswer)
        {
            return;
        }
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
