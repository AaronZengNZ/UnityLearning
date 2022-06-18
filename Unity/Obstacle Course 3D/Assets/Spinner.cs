using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float xSpin = 30f;
    [SerializeField] float ySpin = 30f;
    [SerializeField] float zSpin = 30f;

    // Update is called once per frame
    void Update()
    {
        float xValue = xSpin * Time.deltaTime;
        float yValue = ySpin * Time.deltaTime;
        float zValue = zSpin * Time.deltaTime;
        transform.Rotate(xValue, yValue, zValue);
    }
}
