using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    public GameObject mainCam;
    public GameObject cam1;

    bool mainCamIsOn = true;
    // Update is called once per frame
    void Start()
    {
        cam1.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("SwitchCamera"))
        {
            if (mainCamIsOn)
            {
                cam1.SetActive(true);
                mainCam.SetActive(false);
                mainCamIsOn = false;
                Cursor.visible = true;
            }
            else
            {
                mainCam.SetActive(true);
                cam1.SetActive(false);
                mainCamIsOn = true;
                Cursor.visible = false;
            }
        }
    }
}
