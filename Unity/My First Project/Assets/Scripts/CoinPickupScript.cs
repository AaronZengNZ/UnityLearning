using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickupScript : MonoBehaviour
{

    [SerializeField] int respawnTimer = 10;
    [SerializeField] int coinValue = 1;
    [SerializeField] int gemValue = 0;
    [SerializeField] bool isKey = false;
    [SerializeField] AudioClip coinPickUpSFX;
    [SerializeField] bool isNaturallySpawned = false;
    [SerializeField] float flingPower = 2f;
    bool ableToBePickedUp = false;

    Rigidbody2D myRigidBody;

    void Awake()
    {
        StartCoroutine(CoinPickupDelay());
    }

    void Start() {
        if (!isNaturallySpawned)
        {
            myRigidBody = GetComponent<Rigidbody2D>();
            myRigidBody.velocity = new Vector2(Random.Range(-1.5f, 1.5f), Random.Range(1f, 4f)) * flingPower;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision is PolygonCollider2D)) return;
        else
        {
            if (ableToBePickedUp)
            {
                if (coinPickUpSFX)
                {
                    AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position);
                }
                Destroy(gameObject);
            }
        }
    }

    IEnumerator CoinPickupDelay()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        ableToBePickedUp = true;
    }

    private void OnDestroy()
    {
        if (coinValue > 0)
        {
            CoinsManager.instance.PickUpCoin(coinValue);
        }
        else
        {
            if(gemValue > 0)
            {
                CoinsManager.instance.PickUpGem(gemValue);
            }
        }
        if (isKey)
        {
            CoinsManager.instance.PickUpKey();
        }
    }
}
