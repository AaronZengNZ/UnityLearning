using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RainbowAnim : MonoBehaviour
{
    private Image image;
    public float rainbowSpeed;
    public float saturation = 1f;

    public TextMeshProUGUI rgbText;
    public TextMeshProUGUI rgbStat;
    // Start is called before the first frame update
    void Start()
    {
        //get image
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        image.color = Color.HSVToRGB(Mathf.Repeat(Time.realtimeSinceStartup * rainbowSpeed, 1), saturation, 1);
        rgbText.color = Color.HSVToRGB(Mathf.Repeat(Time.realtimeSinceStartup * rainbowSpeed, 1), saturation, 1);
        rgbStat.color = rgbText.color;


    }
}
