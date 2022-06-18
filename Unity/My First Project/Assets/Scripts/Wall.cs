using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] string wallColor = "purple";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CoinsManager.instance.HasKey())
        {
            Destroy(gameObject);
        }
    }
}
