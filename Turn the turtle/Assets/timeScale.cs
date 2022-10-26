using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeScale : MonoBehaviour
{
    [SerializeField] float timescale = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = timescale;
    }

    // Update is called once per frame
    void Update()
    {
        if(timescale != Time.timeScale)
        {
            Time.timeScale = timescale;
        }
    }
}
