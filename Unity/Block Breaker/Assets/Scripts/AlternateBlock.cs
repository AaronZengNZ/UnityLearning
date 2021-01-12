using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternateBlock : MonoBehaviour
{
    [SerializeField] AudioClip breakSound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (breakSound)
        {
            AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        }
        Destroy(gameObject);
    }
}
