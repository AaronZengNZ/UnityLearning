using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegCat : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        GameObject otherObject = otherCollider.gameObject;

        if (otherObject.GetComponent<BorkyBun>())
        {
            GetComponent<Attacker>().Attack(otherObject);
        }
    }
}
