using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningScript : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    [SerializeField] GameObject exclamationMark;

    // Start is called before the first frame update
    void Start()
    {
        exclamationMark.SetActive(false);
        StartCoroutine(WaitAndFlash());
    }

    IEnumerator WaitAndFlash()
    {
        yield return new WaitForSeconds(delay);
        exclamationMark.SetActive(true);
        yield return new WaitForSeconds(.2f);
        exclamationMark.SetActive(false);
        yield return new WaitForSeconds(.2f);
        exclamationMark.SetActive(true);
        yield return new WaitForSeconds(.2f);
        exclamationMark.SetActive(false);
        yield return new WaitForSeconds(.2f);
        exclamationMark.SetActive(true);
        yield return new WaitForSeconds(.2f);
        exclamationMark.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
