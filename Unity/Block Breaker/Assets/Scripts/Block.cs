using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockExplosionVFX;
    [SerializeField] Sprite[] hitSprites;

    // cached reference
    Level level;

    // state variables
    [SerializeField] int timesHit = 0;

    private void Start()
    {
        if (tag == "Breakable")
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[0];
            level = FindObjectOfType<Level>();
            level.CountBreakableBlocks();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit();     
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array - " + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        PlayBlockDestroySFX();
            level.BlockDestroyed();
            Destroy(gameObject);
            TriggerExplosionVFX();
    }

    private void PlayBlockDestroySFX()
    {
        if (breakSound)
        {
            AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        }
    }

    private void TriggerExplosionVFX()
    {
        FindObjectOfType<GameStatus>().AddToScore();
        GameObject explosion = Instantiate(blockExplosionVFX, transform.position, transform.rotation);
        Destroy(explosion, 1f);
    }
}
