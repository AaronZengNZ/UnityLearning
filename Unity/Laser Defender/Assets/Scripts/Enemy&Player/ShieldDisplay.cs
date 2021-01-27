using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldDisplay : MonoBehaviour
{
    Text shieldText;
    Player player;

    void Start()
    {
        shieldText = GetComponent<Text>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        shieldText.text = player.GetShield().ToString();
    }
}
