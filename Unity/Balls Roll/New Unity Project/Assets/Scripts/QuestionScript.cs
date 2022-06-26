using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionScript : MonoBehaviour
{
    public static QuestionScript Instance { get; private set; }
    [SerializeField] TextMeshPro tmp;
    private TextMeshProUGUI textMeshPro;
    public Button btn1;

    // Start is called before the first frame update
    void Awake()
    {
        Hide();
        Instance = this;
        textMeshPro = tmp.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    public void ShowQuestion(string questionText, Action action1)
    {
        Show();
        textMeshPro.text = questionText;
        btn1.onClick.AddListener(() =>
        {
            Hide();
            action1();
        });
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
