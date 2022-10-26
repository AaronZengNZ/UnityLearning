using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject text1;
    [SerializeField] GameObject text2;
    [SerializeField] GameObject text3;
    [SerializeField] GameObject text4;
    [SerializeField] GameObject text5;
    [SerializeField] TextMeshProUGUI mtext1;
    [SerializeField] TextMeshProUGUI mtext2;
    [SerializeField] TextMeshProUGUI mtext3;
    [SerializeField] TextMeshProUGUI mtext4;
    [SerializeField] TextMeshProUGUI mtext5;
    [SerializeField] bool isTutorial = true;
    [SerializeField] float fadeDelay = 2f;
    // Start is called before the first frame update
    void Start()
    {
        if (isTutorial)
        {
            text1.SetActive(true);
            text2.SetActive(false);
            text3.SetActive(false);
            text4.SetActive(false);
            text5.SetActive(false);
            StartCoroutine(FadeTextToZeroAlpha(0f, mtext2));
            StartCoroutine(FadeTextToZeroAlpha(0f, mtext3));
            StartCoroutine(FadeTextToZeroAlpha(0f, mtext4));
            StartCoroutine(FadeTextToZeroAlpha(0f, mtext5));
        }
        else
        {
            StartCoroutine(fade());
        }
    }

    IEnumerator fade()
    {
        yield return new WaitForSeconds(fadeDelay);
        StartCoroutine(FadeTextToZeroAlpha(1f, mtext1));
        StartCoroutine(FadeTextToZeroAlpha(1f, mtext2));
    }

    public void SetTextActive(float number)
    {
        if(number == 1) {
            StartCoroutine(TextSequence());
        }
        if (number == 2) { text5.SetActive(true); StartCoroutine(FadeAll()); StartCoroutine(FadeTextToFullAlpha(1f, mtext5)); }
    }

    IEnumerator FadeAll()
    {
        StartCoroutine(FadeTextToZeroAlpha(1f, mtext2));
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(FadeTextToZeroAlpha(2f, mtext1));
        StartCoroutine(FadeTextToZeroAlpha(2f, mtext3));
        StartCoroutine(FadeTextToZeroAlpha(3f, mtext4));
        StartCoroutine(FadeTextToZeroAlpha(4f, mtext5));
    }

    IEnumerator TextSequence()
    {
        text2.SetActive(true);
        StartCoroutine(FadeTextToFullAlpha(1f, mtext2));
        yield return new WaitForSeconds(1f);
        text3.SetActive(true); 
        StartCoroutine(FadeTextToFullAlpha(1f, mtext3));
        yield return new WaitForSeconds(1f);
        text4.SetActive(true);
        StartCoroutine(FadeTextToFullAlpha(1f, mtext4));
    }

    public IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
