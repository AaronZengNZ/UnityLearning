using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int ScoreAmount = 10;
    [SerializeField] AudioClip coinPickUpSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision is CapsuleCollider2D)) return;
        AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position);
        FindObjectOfType<Gamesession>().AddToScore(ScoreAmount);
        Destroy(gameObject);
    }
}
