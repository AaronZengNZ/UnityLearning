using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Scope : MonoBehaviour
{
    FirstPersonController fpsController;
    [SerializeField] float zoomOutSensitivity = 2f;
    [SerializeField] float zoomInSensitivity = .5f;

    // Start is called before the first frame update
    void Start()
    {
        fpsController = GetComponentInParent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckScope();
    }

    private void CheckScope()
    {
        if (Input.GetMouseButton(1))
        {
            fpsController.m_MouseLook.XSensitivity = zoomInSensitivity;
            fpsController.m_MouseLook.YSensitivity = zoomInSensitivity;
            GetComponent<Animator>().SetBool("IsScoped", true);
        }
        else
        {
            fpsController.m_MouseLook.XSensitivity = zoomOutSensitivity;
            fpsController.m_MouseLook.YSensitivity = zoomOutSensitivity;
            GetComponent<Animator>().SetBool("IsScoped", false);
        }
    }
}
