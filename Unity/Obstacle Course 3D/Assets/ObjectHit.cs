using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHit : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
            if (gameObject.tag != "Hit")
            {
                gameObject.tag = "Hit";
                other.gameObject.GetComponent<Scorer>().ObjectStrike();
            }
        }
    }
}
