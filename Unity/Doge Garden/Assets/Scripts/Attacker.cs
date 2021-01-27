using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [Range(0f, 5f)]
    float currentSpeed = 1f;
    [SerializeField] int coinRewards = 10;

    void Update()
    {
        transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
    }

    public int GetCoinRewards()
    {
        return coinRewards;
    }

    public void SetMovementSpeed(float speed)
    {
        currentSpeed = speed;
    }
}
