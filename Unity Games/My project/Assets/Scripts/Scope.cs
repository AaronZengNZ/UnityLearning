using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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

            GetComponent<Animator>().SetBool("IsScoped", true);
            Debug.Log("Pressed");
        }
        else
        {
            GetComponent<Animator>().SetBool("IsScoped", false);
        }
    }
}
